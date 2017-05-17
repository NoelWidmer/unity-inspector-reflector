using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorReflector
{
    public class InspectorDrawer
    {
        public readonly Dictionary<string, Func<PropertyAndInspectAttribute, object, object>> _drawLookup;



        public InspectorDrawer()
        {
            _drawLookup = new Dictionary<string, Func<PropertyAndInspectAttribute, object, object>>();

            _drawLookup.Add(typeof(int).AssemblyQualifiedName, DrawInt);
        }



        public bool ShouldReflectInspector(object obj)
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



        public void DrawReflectedInspector(object obj)
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

            DrawProperties(obj, inspectableProperties);
        }



        private void DrawProperties(object obj, List<PropertyAndInspectAttribute> propertyInfos)
        {
            foreach(var propertyInfo in propertyInfos)
            {
                if(propertyInfo.Property.CanRead == false)
                {
                    Warn("The following property cannot be read from: " + propertyInfo.Property.DeclaringType.FullName + "." + propertyInfo.Property.Name);
                }
                else if(propertyInfo.Property.CanWrite == false)
                {
                    EditorGUILayout.LabelField(propertyInfo.Property.Name, propertyInfo.Property.GetValue(obj, null).ToString());
                }
                else
                {
                    object valueOrRef = propertyInfo.Property.GetValue(obj, null);

                    if(valueOrRef.GetType().IsValueType)
                    {
                        valueOrRef = DrawValue(propertyInfo, valueOrRef);
                    }
                    else
                    {
                        valueOrRef = DrawValue(propertyInfo, valueOrRef);
                    }

                    propertyInfo.Property.SetValue(obj, valueOrRef, null);
                }
            }
        }



        private object DrawValue(PropertyAndInspectAttribute propertyInfo, object value)
        {
            string aqtn = value.GetType().AssemblyQualifiedName;

            Func<PropertyAndInspectAttribute, object, object> draw;
            if(_drawLookup.TryGetValue(aqtn, out draw))
            {
                return draw(propertyInfo, value);
            }
            else
            {
                EditorGUILayout.LabelField("The following value type has noo drawer: " + aqtn);
                return null;
            }
        }



        #region Value type Drawers

        private object DrawInt(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.IntField(propertyInfo.Property.Name, (int)value);
        }

        #endregion



        private object DrawReference(PropertyAndInspectAttribute propertyInfo, object reference)
        {
            EditorGUILayout.LabelField("Reflected reference types are not yet supported.");
            return null;
        }



        private void Warn(string message)
        {
            Debug.LogWarning("[Inspection] " + message);
        }
    }
}