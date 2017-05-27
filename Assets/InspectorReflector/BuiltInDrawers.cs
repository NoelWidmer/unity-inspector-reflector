using System;
using UnityEditor;
using UnityEngine;

namespace InspectorReflector
{
    public static class BuiltInDrawers
    {
        public static object DrawAnimationCurve(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.CurveField(propertyInfo.Info.Name, (AnimationCurve)value);
        }

        public static object DrawBool(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.Toggle(propertyInfo.Info.Name, (bool)value);
        }

        public static object DrawByte(PropertyAndInspectAttribute propertyInfo, object value)
        {
            int newValue = EditorGUILayout.IntField(propertyInfo.Info.Name, (byte)value);

            if(newValue < byte.MinValue)
                return byte.MinValue;
            else if(newValue > byte.MaxValue)
                return byte.MaxValue;

            return (byte)newValue;
        }

        public static object DrawBounds(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.BoundsField(propertyInfo.Info.Name, (Bounds)value);
        }

        public static object DrawChar(PropertyAndInspectAttribute propertyInfo, object value)
        {
            char oldValue = (char)value;
            string newValue = EditorGUILayout.TextField(propertyInfo.Info.Name, string.Empty + oldValue);

            if(newValue == null || newValue == string.Empty)
                return default(char);

            return char.Parse(newValue.Substring(0, 1));
        }

        public static object DrawColor(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.ColorField(propertyInfo.Info.Name, (Color)value);
        }

        public static object DrawDouble(PropertyAndInspectAttribute propertyInfo, object value)
        {
            switch(propertyInfo.InspectAttribute.InspectionType)
            {
                case InspectionType.DelayedDouble:
                    return EditorGUILayout.DelayedDoubleField(propertyInfo.Info.Name, (double)value);
            }

            return EditorGUILayout.DoubleField(propertyInfo.Info.Name, (double)value);
        }

        public static object DrawDropableObject(PropertyAndInspectAttribute propertyInfo, UnityEngine.Object value, bool allowSceneObjects)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(propertyInfo.Info.Name);
            UnityEngine.Object result = EditorGUILayout.ObjectField(value, propertyInfo.Info.PropertyType, allowSceneObjects);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        public static object DrawEnum(PropertyAndInspectAttribute propertyInfo, object value)
        {
            var flagAttrs = propertyInfo.Info.PropertyType.GetCustomAttributes(typeof(FlagsAttribute), false);

            if(flagAttrs == null || flagAttrs.Length == 0)
            {
                return EditorGUILayout.EnumPopup(propertyInfo.Info.Name, (Enum)value);
            }
            else if(flagAttrs.Length == 1)
            {
                return EditorGUILayout.EnumMaskField(propertyInfo.Info.Name, (Enum)value);
            }
            else
            {
                Debug.LogWarning("Multiple attributes of type " + typeof(FlagsAttribute).FullName + " found for enum of type " + propertyInfo.Info.PropertyType.FullName);
                return value;
            }
        }

        public static object DrawLayerMask(PropertyAndInspectAttribute propertyInfo, object value)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(propertyInfo.Info.Name);
            int newValue = (EditorGUILayout.LayerField((LayerMask)value));
            EditorGUILayout.EndHorizontal();
            return (LayerMask)newValue;
        }

        public static object DrawFloat(PropertyAndInspectAttribute propertyInfo, object value)
        {
            switch(propertyInfo.InspectAttribute.InspectionType)
            {
                case InspectionType.DelayedFloat:
                    return EditorGUILayout.DelayedFloatField(propertyInfo.Info.Name, (float)value);
            }

            return EditorGUILayout.FloatField(propertyInfo.Info.Name, (float)value);
        }

        public static object DrawInt(PropertyAndInspectAttribute propertyInfo, object value)
        {
            switch(propertyInfo.InspectAttribute.InspectionType)
            {
                case InspectionType.DelayedInt:
                    return EditorGUILayout.DelayedIntField(propertyInfo.Info.Name, (int)value);
            }

            return EditorGUILayout.IntField(propertyInfo.Info.Name, (int)value);
        }

        public static object DrawLong(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.LongField(propertyInfo.Info.Name, (long)value);
        }

        public static object DrawRect(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.RectField(propertyInfo.Info.Name, (Rect)value);
        }

        public static object DrawSByte(PropertyAndInspectAttribute propertyInfo, object value)
        {
            int newValue = EditorGUILayout.IntField(propertyInfo.Info.Name, (sbyte)value);

            if(newValue < sbyte.MinValue)
                return sbyte.MinValue;
            else if(newValue > sbyte.MaxValue)
                return sbyte.MaxValue;

            return (sbyte)newValue;
        }

        public static object DrawShort(PropertyAndInspectAttribute propertyInfo, object value)
        {
            int newValue = EditorGUILayout.IntField(propertyInfo.Info.Name, (short)value);

            if(newValue < short.MinValue)
                return short.MinValue;
            else if(newValue > short.MaxValue)
                return short.MaxValue;

            return (short)newValue;
        }

        public static object DrawString(PropertyAndInspectAttribute propertyInfo, object value)
        {
            switch(propertyInfo.InspectAttribute.InspectionType)
            {
                case InspectionType.DelayedString:
                    return EditorGUILayout.DelayedTextField(propertyInfo.Info.Name, (string)value);

                case InspectionType.TagString:
                    return EditorGUILayout.TagField(propertyInfo.Info.Name, (string)value);

                case InspectionType.AreaString:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel(propertyInfo.Info.Name);
                    string result = EditorGUILayout.TextArea((string)value);
                    EditorGUILayout.EndHorizontal();
                    return result;
            }

            return EditorGUILayout.TextField(propertyInfo.Info.Name, (string)value);
        }

        public static object DrawUInt(PropertyAndInspectAttribute propertyInfo, object value)
        {
            long newValue = EditorGUILayout.LongField(propertyInfo.Info.Name, (uint)value);

            if(newValue < uint.MinValue)
                return uint.MinValue;
            else if(newValue > uint.MaxValue)
                return uint.MaxValue;

            return (uint)newValue;
        }

        public static object DrawULong(PropertyAndInspectAttribute propertyInfo, object value)
        {
            ulong oldValue = (ulong)value;
            string newValue = EditorGUILayout.TextField(propertyInfo.Info.Name, string.Empty + oldValue);

            if(newValue == null || newValue == string.Empty)
                return default(ulong);

            ulong result;
            if(ulong.TryParse(newValue, out result))
                return result;

            Debug.LogWarning("Cannot parse " + newValue + " to " + typeof(ulong).FullName);
            return default(ulong);
        }

        public static object DrawUShort(PropertyAndInspectAttribute propertyInfo, object value)
        {
            int newValue = EditorGUILayout.IntField(propertyInfo.Info.Name, (ushort)value);

            if(newValue < ushort.MinValue)
                return ushort.MinValue;
            else if(newValue > ushort.MaxValue)
                return ushort.MaxValue;

            return (ushort)newValue;
        }

        public static object DrawVector2(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.Vector2Field(propertyInfo.Info.Name, (Vector2)value);
        }

        public static object DrawVector3(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.Vector3Field(propertyInfo.Info.Name, (Vector3)value);
        }

        public static object DrawVector4(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.Vector4Field(propertyInfo.Info.Name, (Vector4)value);
        }
    }
}