using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorReflector
{
    public class InspectorDrawer
    {
        #region Static

        public static readonly Dictionary<string, Func<PropertyAndInspectAttribute, object, object>> _drawersLookup;



        static InspectorDrawer()
        {
            _drawersLookup = new Dictionary<string, Func<PropertyAndInspectAttribute, object, object>>();

            _drawersLookup.Add("$", DefaultDrawers.DrawEnum);

            //TODO register more type drawers.
            RegisterDrawer<Bounds>(DefaultDrawers.DrawBounds);
            RegisterDrawer<Color>(DefaultDrawers.DrawColor);
            RegisterDrawer<AnimationCurve>(DefaultDrawers.DrawCurve);
            RegisterDrawer<double>(DefaultDrawers.DrawDouble);
            RegisterDrawer<float>(DefaultDrawers.DrawFloat);
            RegisterDrawer<int>(DefaultDrawers.DrawInt);
            RegisterDrawer<long>(DefaultDrawers.DrawLong);
            RegisterDrawer<Rect>(DefaultDrawers.DrawRect);
            RegisterDrawer<string>(DefaultDrawers.DrawString);
            RegisterDrawer<bool>(DefaultDrawers.DrawToggle);
            RegisterDrawer<Vector2>(DefaultDrawers.DrawVector2);
            RegisterDrawer<Vector3>(DefaultDrawers.DrawVector3);
            RegisterDrawer<Vector4>(DefaultDrawers.DrawVector4);
        }



        public static void RegisterDrawer<T>(Func<PropertyAndInspectAttribute, object, object> drawer, bool overwrite = false)
        {
            string aqtn = typeof(T).AssemblyQualifiedName;

            if(_drawersLookup.ContainsKey(aqtn))
            {
                if(overwrite == false)
                    throw new InvalidOperationException("A drawer for the following type has already been registered: " + typeof(T).FullName);

                if(drawer != null)
                    _drawersLookup.Remove(aqtn);
            }

            if(drawer == null)
                throw new ArgumentNullException("drawer");

            _drawersLookup.Add(aqtn, drawer);
        }

        #endregion



        private Stack<string> _propertyPath = new Stack<string>();
        private TransientData _transientData;



        public bool ShouldReflectInspector(object target)
        {
            if(target == null)
                throw new ArgumentNullException("target");

            //TODO need better way to recognize inspectable types.
            object[] attributes = target.GetType().GetCustomAttributes(typeof(InspectAttribute), false);

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



        public void ReflectInspector(object target, TransientData transientData)
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

            List<PropertyInfo> nonIndexedProperties;
            {
                nonIndexedProperties = new List<PropertyInfo>();

                foreach(var property in publicInstanceProperties)
                {
                    ParameterInfo[] paramInfos = property.GetIndexParameters();

                    if(paramInfos == null || paramInfos.Length == 0)
                        nonIndexedProperties.Add(property);
                }

                if(nonIndexedProperties == null || nonIndexedProperties.Count == 0)
                    return;
            }


            List<PropertyAndInspectAttribute> properties;
            {
                properties = new List<PropertyAndInspectAttribute>();

                foreach(var property in nonIndexedProperties)
                {
                    //TODO need better way to recognize inspectable properties.
                    object[] attributes = property.GetCustomAttributes(typeof(InspectAttribute), true);

                    if(attributes != null)
                    {
                        if(attributes.Length == 1)
                        {
                            properties.Add(new PropertyAndInspectAttribute(property, (InspectAttribute)attributes[0]));
                        }
                        else if(attributes.Length == 2)
                        {
                            Warn("Found multiple attributes of type " + typeof(InspectAttribute).Name + " on " + property.DeclaringType.FullName + "." + property.Name);
                        }
                    }
                }
            }

            if(properties.Count == 0)
                return;

            _transientData = transientData;
            DrawProperties(target, properties);
        }



        private void DrawProperties(object target, List<PropertyAndInspectAttribute> properties)
        {
            _propertyPath.Clear();

            foreach(var property in properties)
            {
                PropertyInfo propertyInfo = property.Info;
                _propertyPath.Push(propertyInfo.Name);

                if(propertyInfo.CanRead == false)
                {
                    Warn("The following property cannot be read from: " + propertyInfo.DeclaringType.FullName + "." + propertyInfo.Name);
                }
                else if(propertyInfo.CanWrite == false || property.InspectAttribute.InspectAsReadonly)
                {
                    EditorGUILayout.LabelField(propertyInfo.Name, propertyInfo.GetValue(target, null).ToString());
                }
                else
                {
                    Func<PropertyAndInspectAttribute, object, object> drawer;
                    object origValueOrRef = propertyInfo.GetValue(target, null);
                    object newValueOrRef;

                    if(propertyInfo.PropertyType.IsEnum)
                    {
                        newValueOrRef = _drawersLookup["$"](property, origValueOrRef);
                    }
                    else
                    {
                        string aqtn = propertyInfo.PropertyType.AssemblyQualifiedName;

                        if(_drawersLookup.TryGetValue(aqtn, out drawer))
                        {
                            //TODO better null checking (string)
                            if(origValueOrRef != null || propertyInfo.PropertyType == typeof(string))
                            {
                                newValueOrRef = drawer(property, origValueOrRef);
                            }
                            else
                            {
                                DrawNull(property.Info.Name);
                                newValueOrRef = origValueOrRef;
                            }
                        }
                        else
                        {
                            if(origValueOrRef != null && propertyInfo.PropertyType.IsValueType)
                            {
                                Warn("The following value-type has no drawer: " + propertyInfo.DeclaringType.FullName + "." + propertyInfo.Name);
                                newValueOrRef = origValueOrRef;
                            }
                            else
                            {
                                newValueOrRef = DrawReference(property, origValueOrRef);
                            }
                        }
                    }

                    if(origValueOrRef != newValueOrRef)
                        propertyInfo.SetValue(target, newValueOrRef, null);
                }

                _propertyPath.Pop();
            }
        }



        private object DrawReference(PropertyAndInspectAttribute property, object reference)
        {
            if(reference == null)
            {
                DrawNull(property.Info.Name);
            }
            else
            {
                bool show = true;

                if(reference is UnityEngine.Object)
                {
                    show = EditorGUILayout.InspectorTitlebar(show, (UnityEngine.Object)reference);
                }
                else
                {
                    show = EditorGUILayout.Foldout(show, property.Info.PropertyType.Name);
                }

                if(show)
                {
                    EditorGUILayout.LabelField("test");
                }
            }

            //TODO inspect reflected types.
            return null;
        }



        private void DrawNull(string propertyName)
        {
            EditorGUILayout.LabelField(propertyName, "Not set");
        }



        private void Warn(string message)
        {
            Debug.LogWarning("[Inspection] " + message);
        }
    }
}