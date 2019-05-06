using System;
using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class EnumDrawer
{
    public object Draw(IMemberInspectionInfo attr, object value)
    {
        if(value is Enum enum_)
        {
            var flagAttrs = attr.RealType.GetCustomAttributes(typeof(FlagsAttribute), false);

            if(flagAttrs == null || flagAttrs.Length == 0)
            {
                return EditorGUILayout.EnumPopup(attr.Info.Name, enum_);
            }
            else if(flagAttrs.Length == 1)
            {
                return EditorGUILayout.EnumFlagsField(attr.Info.Name, enum_);
            }
            else
            {
                Debug.LogWarning($"Multiple attributes of type {typeof(FlagsAttribute).FullName} found for enum of type {attr.RealType.FullName}.");
                return value;
            }
        }
        else
        {
            throw new ArgumentException("Must be an enum.", nameof(value));
        }
    }
}
