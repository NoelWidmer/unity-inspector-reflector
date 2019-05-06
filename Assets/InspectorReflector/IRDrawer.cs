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
        private static readonly Dictionary<Type, IDrawer> _drawers = new Dictionary<Type, IDrawer>();

        static IRDrawer()
        {
            RegisterDrawer(new AnimationCurveDrawer(), false);
            RegisterDrawer(new BooleanDrawer(), false);
            RegisterDrawer(new BoundsDrawer(), false);
            RegisterDrawer(new ByteDrawer(), false);
            RegisterDrawer(new CharacterDrawer(), false);
            RegisterDrawer(new ColorDrawer(), false);
            RegisterDrawer(new DoubleDrawer(), false);
            RegisterDrawer(new FloatDrawer(), false);
            RegisterDrawer(new IntegerDrawer(), false);
            RegisterDrawer(new LayerMaskDrawer(), false);
            RegisterDrawer(new LongDrawer(), false);
            RegisterDrawer(new RectangleDrawer(), false);
            RegisterDrawer(new ShortDrawer(), false);
            RegisterDrawer(new SignedByteDrawer(), false);
            RegisterDrawer(new StringDrawer(), false);
            RegisterDrawer(new UnsignedIntegerDrawer(), false);
            RegisterDrawer(new UnsignedLongDrawer(), false);
            RegisterDrawer(new UnsignedShortDrawer(), false);
            RegisterDrawer(new Vector2Drawer(), false);
            RegisterDrawer(new Vector3Drawer(), false);
            RegisterDrawer(new Vector4Drawer(), false);
        }

        public static void RegisterDrawer(IDrawer drawer, bool overwritePreviousDrawer)
        {
            if(drawer == null)
                throw new ArgumentNullException(nameof(drawer));

            var targetType = drawer.TargetType;

            if(_drawers.ContainsKey(targetType))
            {
                if(overwritePreviousDrawer == false)
                    throw new InvalidOperationException("A drawer for the following type has already been registered: " + targetType);

                _drawers[targetType] = drawer;
            }
            else
            {
                _drawers.Add(targetType, drawer);
            }
        }

        public bool ShouldDrawObjectOfType(Type targetType)
        {
            if(targetType == null)
                throw new ArgumentNullException(nameof(targetType));
            
            var attributes = targetType.GetCustomAttributes(typeof(EnableIRAttribute), false);

            if(attributes == null || attributes.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public void Draw(object target, Dictionary<string, bool> folderPathToIsExpanded)
        {
            if(target == null)
                throw new ArgumentNullException(nameof(target));

            if(folderPathToIsExpanded == null)
                throw new ArgumentNullException(nameof(folderPathToIsExpanded));
      
            Draw(target, folderPathToIsExpanded, new Stack<string>());
        }

        private void Draw(object target, Dictionary<string, bool> folderPathToIsExpanded, Stack<string> pathStack)
        {
            var (fields, properties) = GetPublicInstanceMembers(target);

            properties = GetNonIndexedProperties(properties);

            var fieldsToInspect = GetFieldsWithInspectAttribute(fields);
            var propertiesToInspect = GetPropertiesWithInspectAttribute(properties);
            var membersToInspect = fieldsToInspect.Union(propertiesToInspect).ToList();

            Draw(target, membersToInspect, folderPathToIsExpanded, pathStack);
        }

        private (List<FieldInfo> fields, List<PropertyInfo> properties) GetPublicInstanceMembers(object target)
        {
            var fields = new List<FieldInfo>();
            var properties = new List<PropertyInfo>();

            var flags = BindingFlags.Public | BindingFlags.Instance;
            var targetType = target.GetType();

            var fields_ = targetType.GetFields(flags);
            var properties_ = targetType.GetProperties(flags);

            if(fields_ != null)
                fields = fields_.ToList();

            if(properties_ != null)
                properties = properties_.ToList();

            return (fields, properties);
        }

        private List<PropertyInfo> GetNonIndexedProperties(List<PropertyInfo> properties)
        {
            var nonIndexedProperties = new List<PropertyInfo>();

            foreach(var property in properties)
            {
                var propertyIndexParams = property.GetIndexParameters();

                if(propertyIndexParams == null || propertyIndexParams.Length == 0)
                    nonIndexedProperties.Add(property);
            }

            return nonIndexedProperties;
        }

        private List<IMemberInspectionInfo> GetFieldsWithInspectAttribute(List<FieldInfo> fields)
        {
            var members = new List<IMemberInspectionInfo>();

            foreach(var field in fields)
            {
                var attrs = field.GetCustomAttributes(typeof(InspectAttribute), false);

                if(attrs != null && attrs.Length > 0)
                {
                    if(attrs.Length == 1)
                    {
                        members.Add(new FieldInspectionInfo(field, (InspectAttribute)attrs[0]));
                    }
                    else
                    {
                        Warn($"Found multiple attributes of type {typeof(InspectAttribute).Name} on {field.DeclaringType.FullName}.{field.Name}");
                    }
                }
            }

            return members;
        }

        private List<IMemberInspectionInfo> GetPropertiesWithInspectAttribute(List<PropertyInfo> properties)
        {
            var members = new List<IMemberInspectionInfo>();

            foreach(var property in properties)
            {
                var attrs = property.GetCustomAttributes(typeof(InspectAttribute), false);

                if(attrs != null && attrs.Length > 0)
                {
                    if(attrs.Length == 1)
                    {
                        members.Add(new PropertyInspectionInfo(property, (InspectAttribute)attrs[0]));
                    }
                    else
                    {
                        Warn($"Found multiple attributes of type {typeof(InspectAttribute).Name} on {property.DeclaringType.FullName}.{property.Name}");
                    }
                }
            }

            return members;
        }

        private void Draw(object target, List<IMemberInspectionInfo> membersToInspect, Dictionary<string, bool> folderPathToIsExpanded, Stack<string> pathStack)
        {
            foreach(var memberToInspect in membersToInspect)
            {
                // Push field or property name to path.
                pathStack.Push(memberToInspect.Info.Name);

                var inspectionKind = memberToInspect.InspectAttribute.InspectionKind;
                var memberInfo = memberToInspect.Info;

                // Warn if we cannot read the value.
                if(memberToInspect.CanRead == false)
                {
                    Warn($"The following field or property cannot be read: {target.GetType().Name}.{memberInfo.Name}");
                }
                // If writing the value has been manually forbidden but the value should be selectable.
                else if(inspectionKind == InspectionKind.ImmutableSelectable)
                {
                    DrawImmutableSelectable(target, memberToInspect, folderPathToIsExpanded, pathStack);
                }
                // If we cannot write the value or it has been manually forbidden.
                else if(inspectionKind == InspectionKind.Immutable || memberToInspect.CanWrite == false)
                {
                    DrawImmutable(target, memberToInspect, folderPathToIsExpanded, pathStack);
                }
                else
                {
                    if(ShouldDrawTypeAsList(memberToInspect.RealType))
                    {
                        // Draw a list that can be modified.
                        DrawIEnumerable(target, memberToInspect, false, folderPathToIsExpanded, pathStack);
                    }
                    else
                    {
                        DrawMutable(target, memberToInspect, folderPathToIsExpanded, pathStack);
                    }
                }

                pathStack.Pop();
            }
        }

        private void DrawImmutableSelectable(object target, IMemberInspectionInfo memberToInspect, Dictionary<string, bool> folderPathToIsExpanded, Stack<string> pathStack)
        {
            if(ShouldDrawTypeAsList(memberToInspect.RealType))
            {
                Warn($"It doesn't make sense to draw an instance of {memberToInspect.Info.ReflectedType.Name} as {InspectionKind.ImmutableSelectable}. It will be drawn as {InspectionKind.Immutable} instead.");
                DrawIEnumerable(target, memberToInspect, true, folderPathToIsExpanded, pathStack);
            }
            else
            {
                // TODO Really draw references as selectable?
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel(memberToInspect.Info.Name);
                string text = memberToInspect.GetValue(target).ToString();
                EditorGUILayout.SelectableLabel(text);
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawImmutable(object target, IMemberInspectionInfo memberToInspect, Dictionary<string, bool> folderPathToIsExpanded, Stack<string> pathStack)
        {
            if(ShouldDrawTypeAsList(memberToInspect.RealType))
            {
                // Lists are only readonly on their surface.
                DrawIEnumerable(target, memberToInspect, true, folderPathToIsExpanded, pathStack);
            }
            else
            {
                // Create a readonly field.
                string text = memberToInspect.GetValue(target).ToString();
                EditorGUILayout.LabelField(memberToInspect.Info.Name, text);
            }
        }

        private void DrawMutable(object target, IMemberInspectionInfo memberToInspect, Dictionary<string, bool> folderPathToIsExpanded, Stack<string> pathStack)
        {
            var obj = memberToInspect.GetValue(target);
            object newObj;

            if(memberToInspect.RealType.IsEnum)
            {
                newObj = new EnumDrawer().Draw(memberToInspect, obj);
            }
            else
            {
                if(_drawers.TryGetValue(memberToInspect.RealType, out var drawer))
                {
                    //TODO better null checking (string)
                    if(obj != null /*|| memberToInspect.RealType == typeof(string)*/)
                    {
                        newObj = drawer.Draw(memberToInspect, obj);
                    }
                    else
                    {
                        DrawNone(memberToInspect.Info.Name);
                        newObj = null;
                    }
                }
                else
                {
                    if(obj != null && memberToInspect.RealType.IsValueType)
                    {
                        Warn($"The following value-type has no registered drawer: {memberToInspect.RealType}");
                        newObj = obj;
                    }
                    else
                    {
                        newObj = DrawMutableReference(memberToInspect, obj, folderPathToIsExpanded, pathStack);
                    }
                }
            }

            if(obj != newObj)
                memberToInspect.SetValue(target, newObj);
        }

        private object DrawMutableReference(IMemberInspectionInfo fieldOrPropertyInfo, object reference, Dictionary<string, bool> foldoutData, Stack<string> pathStack)
        {
            var inspectionType = fieldOrPropertyInfo.InspectAttribute.InspectionKind;

            if(reference is UnityEngine.Object obj && inspectionType == InspectionKind.DropableObject)
            {
                return new DropableObjectDrawer().Draw(fieldOrPropertyInfo, obj, false);
            }
            else if(reference is UnityEngine.Object obj_ && inspectionType == InspectionKind.DropableObjectAllowSceneObjects)
            {
                return new DropableObjectDrawer().Draw(fieldOrPropertyInfo, obj_, true);
            }
            else
            {
                if(reference == null)
                {
                    DrawNone(fieldOrPropertyInfo.Info.Name);
                    return null;
                }
                else
                {
                    var showReferenceMembers = GetOrCreateFoldoutVisibility(pathStack, foldoutData);

                    if(showReferenceMembers = EditorGUILayout.Foldout(showReferenceMembers, fieldOrPropertyInfo.Info.Name))
                    {
                        EditorGUI.indentLevel++;
                        {
                            if(reference is IEnumerable<object> enumerable)
                            {
                                foreach(var item in enumerable)
                                {
                                    Draw(item, foldoutData, pathStack);
                                }
                            }
                            else
                            {
                                Draw(reference, foldoutData, pathStack);
                            }
                        }
                        EditorGUI.indentLevel--;
                    }

                    UpdateFoldoutVisibility(pathStack, foldoutData, showReferenceMembers);
                    return reference;
                }
            }
        }



        private void DrawIEnumerable(object target, IMemberInspectionInfo fieldOrPropertyInfo, bool immutable, Dictionary<string, bool> foldoutData, Stack<string> pathStack)
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
                            DrawNone($"Item {index + 1}");
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
                                        Draw(item, foldoutData, pathStack);
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
            var path = String.Join(".", ReverseStack(pathStack));

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



        private void DrawNone(string propertyName)
        {
            EditorGUILayout.LabelField(propertyName, "None");
        }



        private void Warn(string message)
        {
            Debug.LogWarning("[Inspection] " + message);
        }
    }
}