using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorReflector
{
    public static class DefaultDrawers
    {
        public static object DrawBounds(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.BoundsField(propertyInfo.Info.Name, (Bounds)value);
        }

        public static object DrawColor(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.ColorField(propertyInfo.Info.Name, (Color)value);
        }

        public static object DrawCurve(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.CurveField(propertyInfo.Info.Name, (AnimationCurve)value);
        }

        public static object DrawDouble(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.DoubleField(propertyInfo.Info.Name, (double)value);
        }

        public static object DrawFloat(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.FloatField(propertyInfo.Info.Name, (float)value);
        }

        public static object DrawInt(PropertyAndInspectAttribute propertyInfo, object value)
        {
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

        public static object DrawText(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.TextField(propertyInfo.Info.Name, (string)value);
        }

        public static object DrawToggle(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.Toggle(propertyInfo.Info.Name, (bool)value);
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