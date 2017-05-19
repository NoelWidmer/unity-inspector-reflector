using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorReflector
{
    public class InspectorDrawer
    {
        public static readonly Dictionary<string, Func<PropertyAndInspectAttribute, object, object>> _drawersLookup;



        static InspectorDrawer()
        {
            _drawersLookup = new Dictionary<string, Func<PropertyAndInspectAttribute, object, object>>();

            //TODO register more type drawers.
            RegisterDrawer<Bounds>(DefaultDrawers.DrawBounds);
            RegisterDrawer<Color>(DefaultDrawers.DrawColor);
            RegisterDrawer<AnimationCurve>(DefaultDrawers.DrawCurve);
            RegisterDrawer<double>(DefaultDrawers.DrawDouble);
            RegisterDrawer<float>(DefaultDrawers.DrawFloat);
            RegisterDrawer<int>(DefaultDrawers.DrawInt);
            RegisterDrawer<Rect>(DefaultDrawers.DrawRect);
            RegisterDrawer<string>(DefaultDrawers.DrawText);
            RegisterDrawer<bool>(DefaultDrawers.DrawToggle);
            RegisterDrawer<Vector2>(DefaultDrawers.DrawVector2);
            RegisterDrawer<Vector3>(DefaultDrawers.DrawVector3);
            RegisterDrawer<Vector4>(DefaultDrawers.DrawVector4);
        }



        public static void RegisterDrawer<T>(Func<PropertyAndInspectAttribute, object, object> drawer, bool overwrite = false)
        {
            if(drawer == null)
                throw new ArgumentNullException("drawer");

            string aqtn = typeof(T).AssemblyQualifiedName;

            if(_drawersLookup.ContainsKey(aqtn))
            {
                if(overwrite == false)
                    throw new InvalidOperationException("A drawer for the following type has already been registered: " + typeof(T).FullName);

                _drawersLookup.Remove(aqtn);
            }

            _drawersLookup.Add(aqtn, drawer);
        }



        public bool ShouldReflectInspector(object target)
        {
            if(target == null)
                throw new ArgumentNullException("target");

            //TODO need better way to recognize inspectable types.
            object[] attributes = target.GetType().GetCustomAttributes(typeof(InspectAttribute), true);

            if(attributes == null || attributes.Length == 0)
            {
                return false;
            }
            else if(attributes.Length == 1)
            {
                return true;
            }
            else
            {
                Warn("Found multiple attributes of type " + typeof(InspectAttribute).Name + " on " + target.GetType().FullName);
                return false;
            }
        }



        public void ReflectInspector(object target)
        {
            if(target == null)
                throw new ArgumentNullException("target");

            PropertyInfo[] publicInstanceProperties;
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

                publicInstanceProperties = target.GetType().GetProperties(flags);

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
                    //TODO need better way to recognize inspectable properties.
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

            DrawProperties(target, inspectableProperties);
        }



        private void DrawProperties(object target, List<PropertyAndInspectAttribute> propertyInfos)
        {
            foreach(var propertyInfo in propertyInfos)
            {
                if(propertyInfo.PropertyInfo.CanRead == false)
                {
                    Warn("The following property cannot be read from: " + propertyInfo.PropertyInfo.DeclaringType.FullName + "." + propertyInfo.PropertyInfo.Name);
                }
                else if(propertyInfo.PropertyInfo.CanWrite == false)
                {
                    EditorGUILayout.LabelField(propertyInfo.PropertyInfo.Name, propertyInfo.PropertyInfo.GetValue(target, null).ToString());
                }
                else
                {
                    object valueOrRef = propertyInfo.PropertyInfo.GetValue(target, null);
                    
                    if(valueOrRef != null && valueOrRef.GetType().IsValueType)
                    {
                        valueOrRef = DrawValue(propertyInfo, valueOrRef);
                    }
                    else
                    {
                        valueOrRef = DefaultDrawers.DrawReference(propertyInfo, valueOrRef);
                    }

                    propertyInfo.PropertyInfo.SetValue(target, valueOrRef, null);
                }
            }
        }



        private object DrawValue(PropertyAndInspectAttribute propertyInfo, object value)
        {
            string aqtn = value.GetType().AssemblyQualifiedName;

            Func<PropertyAndInspectAttribute, object, object> draw;
            if(_drawersLookup.TryGetValue(aqtn, out draw))
            {
                return draw(propertyInfo, value);
            }
            else
            {
                EditorGUILayout.LabelField("The following value type has noo drawer: " + aqtn);
                return null;
            }
        }



        private void Warn(string message)
        {
            Debug.LogWarning("[Inspection] " + message);
        }
    }
}