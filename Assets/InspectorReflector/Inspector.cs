using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using UnityEngine;

namespace InspectorReflector
{
    [CustomEditor(typeof(object), true)]
    public class Inspector : Editor
    {
        public override void OnInspectorGUI()
        {
            object obj = target;

            if(obj == null)
                return;

            if(ShouldReflectInspector(obj))
            {
                DrawReflectedInspector(obj);
            }
            else
            {
                DrawDefaultInspector();
            }
        }



        private bool ShouldReflectInspector(object obj)
        {
            object[] attributes = obj.GetType().GetCustomAttributes(typeof(InspectAttribute), true);

            if(attributes == null)
            {
                return false;
            }
            else if(attributes.Length == 1)
            {
                return true;
            }
            else
            {
                Warn("Found multiple attributes of type " + typeof(InspectAttribute).Name + " on " + obj.GetType().FullName);
                return false;
            }
        }



        private static void DrawReflectedInspector(object obj)
        {
            PropertyInfo[] publicInstanceProperties;
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

                publicInstanceProperties = obj.GetType().GetProperties(flags);

                if(publicInstanceProperties == null || publicInstanceProperties.Length == 0)
                    return;
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
                    return;
            }


            List<PropertyAndInspectAttribute> inspectableProperties;
            {
                inspectableProperties = new List<PropertyAndInspectAttribute>();

                foreach(var property in nonIndexedPropoerties)
                {
                    object[] attributes = property.GetCustomAttributes(typeof(InspectAttribute), true);

                    if(attributes != null)
                    {
                        if(attributes.Length == 1)
                        {
                            inspectableProperties.Add(new PropertyAndInspectAttribute(property, (InspectAttribute)attributes[0]));
                        }
                        else if(attributes.Length == 2)
                        {
                            Warn("Found multiple attributes of type " + typeof(InspectAttribute).Name + " on " + property.DeclaringType.FullName + "." + property.Name);
                        }
                    }
                }
            }

            if(inspectableProperties.Count == 0)
                return;

            DrawInspectableProperties(inspectableProperties);
        }



        private static void DrawInspectableProperties(List<PropertyAndInspectAttribute> propertyInfos)
        {
            foreach(var propertyInfo in propertyInfos)
            {
                if(propertyInfo.Property.CanRead == false)
                {
                    Warn("The following property cannot be read from: " + propertyInfo.Property.DeclaringType.FullName + "." + propertyInfo.Property.Name);
                }
                else if(propertyInfo.Property.CanWrite == false)
                {
                    EditorGUILayout.LabelField(propertyInfo.Property.Name, "");
                }
                else
                {
                    // draw property
                }
            }
        }



        private static void Warn(string message)
        {
            Debug.LogWarning("[Inspection] " + message);
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