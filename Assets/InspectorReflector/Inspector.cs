using System.Collections.Generic;
using UnityEditor;
using System.Reflection;

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

            if(obj is IInspectable)
            {
                DrawReflectedInspector((IInspectable)obj);
            }
            else
            {
                DrawDefaultInspector();
            }
        }



        private static void DrawReflectedInspector(IInspectable obj)
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
                            //log
                        }
                    }
                }

                if(inspectableProperties.Count == 0)
                    return;
            }

            DrawInspectableProperties(inspectableProperties);
        }



        private static void DrawInspectableProperties(List<PropertyAndInspectAttribute> properties)
        {
            foreach(var property in properties)
            {
                EditorGUILayout.TextField(property.Property.Name, "");
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