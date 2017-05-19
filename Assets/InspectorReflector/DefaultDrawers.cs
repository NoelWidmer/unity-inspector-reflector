using UnityEditor;
using UnityEngine;

namespace InspectorReflector
{
    public static class DefaultDrawers
    {
        public static object DrawBounds(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.BoundsField(propertyInfo.PropertyInfo.Name, (Bounds)value);
        }

        public static object DrawColor(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.ColorField(propertyInfo.PropertyInfo.Name, (Color)value);
        }

        public static object DrawCurve(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.CurveField(propertyInfo.PropertyInfo.Name, (AnimationCurve)value);
        }

        public static object DrawDouble(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.DoubleField(propertyInfo.PropertyInfo.Name, (double)value);
        }

        public static object DrawFloat(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.FloatField(propertyInfo.PropertyInfo.Name, (float)value);
        }

        public static object DrawInt(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.IntField(propertyInfo.PropertyInfo.Name, (int)value);
        }

        public static object DrawLong(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.LongField(propertyInfo.PropertyInfo.Name, (long)value);
        }

        public static object DrawRect(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.RectField(propertyInfo.PropertyInfo.Name, (Rect)value);
        }

        public static object DrawText(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.TextField(propertyInfo.PropertyInfo.Name, (string)value);
        }

        public static object DrawToggle(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.Toggle(propertyInfo.PropertyInfo.Name, (bool)value);
        }

        public static object DrawVector2(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.Vector2Field(propertyInfo.PropertyInfo.Name, (Vector2)value);
        }

        public static object DrawVector3(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.Vector3Field(propertyInfo.PropertyInfo.Name, (Vector3)value);
        }

        public static object DrawVector4(PropertyAndInspectAttribute propertyInfo, object value)
        {
            return EditorGUILayout.Vector4Field(propertyInfo.PropertyInfo.Name, (Vector4)value);
        }

        public static object DrawReference(PropertyAndInspectAttribute propertyInfo, object reference)
        {
            if(reference == null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(propertyInfo.PropertyInfo.Name);
                bool instatiate = GUILayout.Button("Instantiate " + propertyInfo.PropertyInfo.PropertyType.Name);
                EditorGUILayout.EndHorizontal();

                if(instatiate)
                {

                }
            }
            else
            {
                bool show = true;

                if(reference is Object)
                {
                    show = EditorGUILayout.InspectorTitlebar(show, (Object)reference);
                }
                else
                {
                    show = EditorGUILayout.Foldout(show, propertyInfo.PropertyInfo.PropertyType.Name);
                }

                if(show)
                {
                    EditorGUILayout.LabelField("test");
                }
            }

            //TODO inspect reflected types.
            return null;
        }
    }
}