using System;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;

namespace InspectorReflector
{
    [CustomEditor(typeof(object), true)]
    public class Inspector : Editor
    {
        private static HashSet<string> _namespaces2Ignore = new HashSet<string>
        {
            "UnityEditor"
        };



        public override void OnInspectorGUI()
        {
            object obj = target;

            if(obj == null)
                return;

            if(ShouldDrawDefaultInspector(obj))
            {
                DrawDefaultInspector();
            }
            else
            {
                DrawReflectedInspector(obj);
            }
        }



        private static bool ShouldDrawDefaultInspector(object obj)
        {
            string ns = obj.GetType().Namespace;
            return _namespaces2Ignore.Contains(ns);
        }



        private static void DrawReflectedInspector(object obj)
        {
            PropertyInfo[] publicInstanceProperties;
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

                publicInstanceProperties = obj.GetType().GetProperties(flags);

                if(publicInstanceProperties == null || publicInstanceProperties.Length == 0)
                {
                    EditorGUILayout.LabelField("No public instance properties found.");
                    return;
                }
            }

            List<PropertyInfo> nonIndexedPropoerties;
            {
                nonIndexedPropoerties = new List<PropertyInfo>();

                foreach(var property in publicInstanceProperties)
                {
                    ParameterInfo[] paramInfos = property.GetIndexParameters();

                    if(paramInfos == null || paramInfos.Length == 0)
                        nonIndexedPropoerties.Add(property);
                }

                if(nonIndexedPropoerties == null || nonIndexedPropoerties.Count == 0)
                {
                    EditorGUILayout.LabelField("Only indexed properties found.");
                    return;
                }
            }
            
            List<PropertyAndInspectAttribute> inspectableProperties = GetInspectableProperties(nonIndexedPropoerties);

            if(inspectableProperties != null && inspectableProperties.Count != 0)
            {
                DrawInspectableProperties(inspectableProperties);
            }
            else
            {
                EditorGUILayout.LabelField("No inspectable properties found.");
            }
        }



        private static List<PropertyAndInspectAttribute> GetInspectableProperties(List<PropertyInfo> properties)
        {
            var result = new List<PropertyAndInspectAttribute>();

            foreach(var property in properties)
            {
                object[] attributes = property.GetCustomAttributes(typeof(InspectAttribute), true);

                if(attributes == null || attributes.Length == 0)
                {
                    //Do nothing
                }
                else if(attributes.Length == 1)
                {
                    result.Add(new PropertyAndInspectAttribute(property, (InspectAttribute)attributes[0]));
                }
                else
                {
                    EditorGUILayout.LabelField("Found multiple " + typeof(InspectAttribute).Name + "s for property " + property.Name);
                }
            }

            return result;
        }



        private static void DrawInspectableProperties(List<PropertyAndInspectAttribute> properties)
        {
            foreach(var property in properties)
            {
                EditorGUILayout.LabelField(property.Property.Name, "");
            }
        }



        private class PropertyAndInspectAttribute
        {
            public PropertyAndInspectAttribute(PropertyInfo property, InspectAttribute inspectAttribute)
            {
                Property = property;
                InspectAttribute = inspectAttribute;
            }

            public PropertyInfo Property
            {
                get;
                private set;
            }
            public InspectAttribute InspectAttribute
            {
                get;
                private set;
            }
        }
    }
}