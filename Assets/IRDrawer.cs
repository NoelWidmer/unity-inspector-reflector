using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorReflector.Implementation
{
    public class IRDrawer
    {
        #region Static

        public static readonly Dictionary<string, Func<MemberInfoAndInspectAttr, object, object>> _drawersLookup;



        static IRDrawer()
        {
            _drawersLookup = new Dictionary<string, Func<MemberInfoAndInspectAttr, object, object>>();

            _drawersLookup.Add("$", BuiltInDrawers.DrawEnum);
            
            RegisterDrawer<AnimationCurve>(BuiltInDrawers.DrawAnimationCurve);
            RegisterDrawer<bool>(BuiltInDrawers.DrawBool);
            RegisterDrawer<byte>(BuiltInDrawers.DrawByte);
            RegisterDrawer<Bounds>(BuiltInDrawers.DrawBounds);
            RegisterDrawer<char>(BuiltInDrawers.DrawChar);
            RegisterDrawer<Color>(BuiltInDrawers.DrawColor);
            RegisterDrawer<double>(BuiltInDrawers.DrawDouble);
            RegisterDrawer<float>(BuiltInDrawers.DrawFloat);
            RegisterDrawer<int>(BuiltInDrawers.DrawInt);
            RegisterDrawer<LayerMask>(BuiltInDrawers.DrawLayerMask);
            RegisterDrawer<long>(BuiltInDrawers.DrawLong);
            RegisterDrawer<Rect>(BuiltInDrawers.DrawRect);
            RegisterDrawer<sbyte>(BuiltInDrawers.DrawSByte);
            RegisterDrawer<short>(BuiltInDrawers.DrawShort);
            RegisterDrawer<string>(BuiltInDrawers.DrawString);
            RegisterDrawer<uint>(BuiltInDrawers.DrawUInt);
            RegisterDrawer<ulong>(BuiltInDrawers.DrawULong);
            RegisterDrawer<ushort>(BuiltInDrawers.DrawUShort);
            RegisterDrawer<Vector2>(BuiltInDrawers.DrawVector2);
            RegisterDrawer<Vector3>(BuiltInDrawers.DrawVector3);
            RegisterDrawer<Vector4>(BuiltInDrawers.DrawVector4);
        }



        public static void RegisterDrawer<T>(Func<MemberInfoAndInspectAttr, object, object> drawer, bool overwrite = false)
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



        /// <summary>
        ///     Returns true if <paramref name="targetType"/> can be drawn with the IR.
        /// </summary>
        /// <param name="targetType">The type to check compatability for.</param>
        public bool SupportsType(Type targetType)
        {
            if(targetType == null)
                throw new ArgumentNullException(nameof(targetType));
            
            object[] attributes = targetType.GetCustomAttributes(typeof(EnableIRAttribute), false);

            if(attributes == null || attributes.Length == 0)
                return false;
            else
                return true;
        }




        public Dictionary<string, bool> DrawInspector(object target)
        {
            Dictionary<string, bool> foldoutData = new Dictionary<string, bool>();
            DrawInspector(target, foldoutData);
            return foldoutData;
        }



        /// <summary>
        ///     Draws <paramref name="target"/> with the IR.
        /// </summary>
        /// <param name="target">The instance to draw the inspector for.</param>
        public void DrawInspector(object target, Dictionary<string, bool> foldoutData)
        {
            if(target == null)
                throw new ArgumentNullException(nameof(target));

            if(foldoutData == null)
                throw new ArgumentNullException(nameof(foldoutData));
      
            DrawInspectorRecursable(target, foldoutData, new Stack<string>());
        }



        private void DrawInspectorRecursable(object target, Dictionary<string, bool> foldoutData, Stack<string> pathStack)
        {
            // Get all fields and properties to consider.
            var fields2Consider = new List<FieldInfo>();
            var properties2Consider = new List<PropertyInfo>();
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                Type targetType = target.GetType();

                FieldInfo[] publicInstanceFields = targetType.GetFields(flags);
                if(publicInstanceFields != null && publicInstanceFields.Length > 0)
                    fields2Consider = publicInstanceFields.ToList();

                PropertyInfo[] publicInstanceProperties = targetType.GetProperties(flags);
                if(publicInstanceProperties != null || publicInstanceProperties.Length > 0)
                    properties2Consider = publicInstanceProperties.ToList();

                if(fields2Consider.Count + properties2Consider.Count == 0)
                    return;
            }

            // Do not consider indexed properties.
            if(properties2Consider != null)
            {
                var nonIndexedProperties2Consider = new List<PropertyInfo>();

                foreach(var property2Consider in properties2Consider)
                {
                    ParameterInfo[] propertyIndexParams = property2Consider.GetIndexParameters();

                    if(propertyIndexParams == null || propertyIndexParams.Length == 0)
                        nonIndexedProperties2Consider.Add(property2Consider);
                }

                if(nonIndexedProperties2Consider.Count > 0)
                {
                    properties2Consider = nonIndexedProperties2Consider;
                }
                else
                {
                    if(fields2Consider.Count == 0)
                        return;

                    properties2Consider.Clear();
                }
            }

            var fieldsAndPropertiesToDraw = new List<MemberInfoAndInspectAttr>();

            // Draw only fields with the inspect attribute.
            if(fields2Consider != null)
            {
                foreach(var field2Consider in fields2Consider)
                {
                    object[] inspectAttributes = field2Consider.GetCustomAttributes(typeof(InspectAttribute), false);

                    if(inspectAttributes != null && inspectAttributes.Length > 0)
                    {
                        if(inspectAttributes.Length == 1)
                        {
                            fieldsAndPropertiesToDraw.Add(new FieldInfoAndInspectAttr(field2Consider, (InspectAttribute)inspectAttributes[0]));
                        }
                        else
                        {
                            Warn($"Found multiple attributes of type {typeof(InspectAttribute).Name} on {field2Consider.DeclaringType.FullName}.{field2Consider.Name}");
                        }
                    }
                }
            }

            // Draw only properties with the inspect attribute.
            if(properties2Consider != null)
            {
                foreach(var property2Consider in properties2Consider)
                {
                    object[] inspectAttributes = property2Consider.GetCustomAttributes(typeof(InspectAttribute), false);

                    if(inspectAttributes != null && inspectAttributes.Length > 0)
                    {
                        if(inspectAttributes.Length == 1)
                        {
                            fieldsAndPropertiesToDraw.Add(new PropertyInfoAndInspectAttr(property2Consider, (InspectAttribute)inspectAttributes[0]));
                        }
                        else
                        {
                            Warn($"Found multiple attributes of type {typeof(InspectAttribute).Name} on {property2Consider.DeclaringType.FullName}.{property2Consider.Name}");
                        }
                    }
                }
            }

            if(fieldsAndPropertiesToDraw.Count == 0)
                return;

            DrawFieldsAndProperties(target, fieldsAndPropertiesToDraw, foldoutData, pathStack);
        }



        /// <summary>
        ///     Draws all the fields and properties in <paramref name="fieldOrPropertyInfos"/>.
        /// </summary>
        /// <param name="target">The instance to draw.</param>
        /// <param name="fieldOrPropertyInfos">The fields and properties to draw.</param>
        private void DrawFieldsAndProperties(object target, IEnumerable<MemberInfoAndInspectAttr> fieldOrPropertyInfos, Dictionary<string, bool> foldoutData, Stack<string> pathStack)
        {
            foreach(var fieldOrPropertyInfo in fieldOrPropertyInfos)
            {
                // Push field or property name to path.
                pathStack.Push(fieldOrPropertyInfo.Info.Name);

                try
                {
                    InspectionKind inspectionKind = fieldOrPropertyInfo.InspectAttribute.InspectionKind;
                    MemberInfo memberInfo = fieldOrPropertyInfo.Info;

                    // Warn if we cannot read the value.
                    if(fieldOrPropertyInfo.CanRead == false)
                    {
                        Warn($"The following field or property cannot be read from: {target.GetType().Name}.{memberInfo.Name}");
                    }
                    // If writing the value has been manually forbidden but the value should be selectable.
                    else if(inspectionKind == InspectionKind.ReadonlySelectable)
                    {
                        if(ShouldDrawTypeAsList(fieldOrPropertyInfo.RealType))
                        {
                            Warn($"It doesn't make sense to draw an instance of {memberInfo.ReflectedType.Name} as {InspectionKind.ReadonlySelectable}. It will be drawn as {InspectionKind.Readonly} instead.");
                            DrawIEnumerable(target, fieldOrPropertyInfo, true, foldoutData, pathStack);
                        }
                        else
                        {
                            // TODO Really draw references as selectable?
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.PrefixLabel(memberInfo.Name);
                            string text = fieldOrPropertyInfo.GetValue(target).ToString();
                            EditorGUILayout.SelectableLabel(text);
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                    // If we cannot write the value or it has been manually forbidden.
                    else if(inspectionKind == InspectionKind.Readonly || fieldOrPropertyInfo.CanWrite == false)
                    {
                        if(ShouldDrawTypeAsList(fieldOrPropertyInfo.RealType))
                        {
                            // Lists are only readonly on their surface.
                            DrawIEnumerable(target, fieldOrPropertyInfo, true, foldoutData, pathStack);
                        }
                        else
                        {
                            // Create a readonly field.
                            string text = fieldOrPropertyInfo.GetValue(target).ToString();
                            EditorGUILayout.LabelField(memberInfo.Name, text);
                        }
                    }
                    else
                    {
                        if(ShouldDrawTypeAsList(fieldOrPropertyInfo.RealType))
                        {
                            // Draw a list that can be modified.
                            DrawIEnumerable(target, fieldOrPropertyInfo, false, foldoutData, pathStack);
                        }
                        else
                        {
                            object origValueOrRef = fieldOrPropertyInfo.GetValue(target);
                            object newValueOrRef;

                            Func<MemberInfoAndInspectAttr, object, object> drawer;

                            if(fieldOrPropertyInfo.RealType.IsEnum)
                            {
                                newValueOrRef = _drawersLookup["$"](fieldOrPropertyInfo, origValueOrRef);
                            }
                            else
                            {
                                string aqtn = fieldOrPropertyInfo.RealType.AssemblyQualifiedName;

                                if(_drawersLookup.TryGetValue(aqtn, out drawer))
                                {
                                    //TODO better null checking (string)
                                    if(origValueOrRef != null || fieldOrPropertyInfo.RealType == typeof(string))
                                    {
                                        newValueOrRef = drawer(fieldOrPropertyInfo, origValueOrRef);
                                    }
                                    else
                                    {
                                        DrawNull(memberInfo.Name);
                                        newValueOrRef = origValueOrRef;
                                    }
                                }
                                else
                                {
                                    if(origValueOrRef != null && fieldOrPropertyInfo.RealType.IsValueType)
                                    {
                                        Warn("The following value-type has no drawer: " + memberInfo.DeclaringType.FullName + "." + memberInfo.Name);
                                        newValueOrRef = origValueOrRef;
                                    }
                                    else
                                    {
                                        newValueOrRef = DrawReference(fieldOrPropertyInfo, origValueOrRef, foldoutData, pathStack);
                                    }
                                }
                            }

                            if(origValueOrRef != newValueOrRef)
                                fieldOrPropertyInfo.SetValue(target, newValueOrRef);
                        }
                    }
                }
                finally
                {
                    // Pop field or property name from path.
                    pathStack.Pop();
                }
            }
        }



        private object DrawReference(MemberInfoAndInspectAttr fieldOrPropertyInfo, object reference, Dictionary<string, bool> foldoutData, Stack<string> pathStack)
        {
            InspectionKind inspectionType = fieldOrPropertyInfo.InspectAttribute.InspectionKind;

            if(reference is UnityEngine.Object && inspectionType == InspectionKind.DropableObject)
            {
                return BuiltInDrawers.DrawDropableObject(fieldOrPropertyInfo, (UnityEngine.Object)reference, false);
            }
            else if(reference is UnityEngine.Object && inspectionType == InspectionKind.DropableObjectAllowSceneObjects)
            {
                return BuiltInDrawers.DrawDropableObject(fieldOrPropertyInfo, (UnityEngine.Object)reference, true);
            }
            else
            {
                if(reference == null)
                {
                    DrawNull(fieldOrPropertyInfo.Info.Name);
                    return null;
                }
                else
                {
                    bool showReferenceMembers = GetOrCreateFoldoutVisibility(pathStack, foldoutData);

                    if(showReferenceMembers = EditorGUILayout.Foldout(showReferenceMembers, fieldOrPropertyInfo.Info.Name))
                    {
                        EditorGUI.indentLevel++;
                        {
                            if(reference is IEnumerable<object>)
                            {
                                var enumerable = (IEnumerable<object>)reference;
                                foreach(var item in enumerable)
                                {
                                    DrawInspectorRecursable(item, foldoutData, pathStack);
                                }
                            }
                            else
                            {
                                DrawInspectorRecursable(reference, foldoutData, pathStack);
                            }
                        }
                        EditorGUI.indentLevel--;
                    }

                    UpdateFoldoutVisibility(pathStack, foldoutData, showReferenceMembers);
                    return reference;
                }
            }
        }



        private void DrawIEnumerable(object target, MemberInfoAndInspectAttr fieldOrPropertyInfo, bool readOnly, Dictionary<string, bool> foldoutData, Stack<string> pathStack)
        {
            var enumerable = (IEnumerable)fieldOrPropertyInfo.GetValue(target);

            bool showItems = GetOrCreateFoldoutVisibility(pathStack, foldoutData);

            if(showItems = EditorGUILayout.Foldout(showItems, fieldOrPropertyInfo.Info.Name))
            {
                EditorGUI.indentLevel++;
                {
                    int index = 0;
                    foreach(var item in enumerable)
                    {
                        if(item == null)
                        {
                            DrawNull($"Item {index + 1}");
                        }
                        else
                        {
                            // Push item index to path.
                            pathStack.Push(index.ToString());

                            try
                            {
                                string itemInspectorName = null;

                                if(item is IInspectableCollectionItem)
                                {
                                    var inspectableCollectionItem = (IInspectableCollectionItem)item;
                                    itemInspectorName = inspectableCollectionItem.InspectorName;

                                    if(itemInspectorName != null && itemInspectorName.Trim() == "")
                                        itemInspectorName = null;
                                }

                                if(itemInspectorName == null)
                                    itemInspectorName = $"Item {index + 1}";

                                bool showItemContent = GetOrCreateFoldoutVisibility(pathStack, foldoutData);

                                if(showItemContent = EditorGUILayout.Foldout(showItemContent, itemInspectorName))
                                {
                                    EditorGUI.indentLevel++;
                                    {
                                        DrawInspectorRecursable(item, foldoutData, pathStack);
                                    }
                                    EditorGUI.indentLevel--;
                                }

                                UpdateFoldoutVisibility(pathStack, foldoutData, showItemContent);
                            }
                            finally
                            {
                                // Pop item index from path.
                                pathStack.Pop();
                            }
                        }

                        index += 1;
                    }

                    if(index == 0)
                        EditorGUILayout.LabelField("This collection is empty.");
                }
                EditorGUI.indentLevel--;
            }
            
            UpdateFoldoutVisibility(pathStack, foldoutData, showItems);
        }



        private bool GetOrCreateFoldoutVisibility(Stack<string> pathStack, Dictionary<string, bool> foldoutData)
        {
            string path = String.Join(".", ReverseStack(pathStack));

            bool showItems;
            if(foldoutData.TryGetValue(path, out showItems) == false)
            {
                showItems = false;
                foldoutData.Add(path, showItems);
            }

            return showItems;
        }



        private IEnumerable<string> ReverseStack(Stack<string> pathStack)
        {
            var reveresedList =  pathStack.ToList();
            reveresedList.Reverse();
            return reveresedList;
        }



        private void UpdateFoldoutVisibility(Stack<string> pathStack, Dictionary<string, bool> foldoutData, bool newValue)
        {
            string path = String.Join(".", ReverseStack(pathStack));
            foldoutData[path] = newValue;
        }



        private bool ShouldDrawTypeAsList(Type type)
        {
            if(type == null)
                throw new ArgumentNullException("type");

            return type.GetInterfaces().Contains(typeof(IEnumerable)) && type != typeof(string);
        }



        private void DrawNull(string propertyName)
        {
            EditorGUILayout.LabelField(propertyName, "Not set (null).");
        }



        private void Warn(string message)
        {
            Debug.LogWarning("[Inspection] " + message);
        }
    }
}