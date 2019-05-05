using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class UnsignedLongDrawer : IDrawer<ulong>
{
    public override ulong Draw(IMemberInspectionInfo attr, ulong value)
    {
        string newValue = EditorGUILayout.TextField(attr.Info.Name, value.ToString());

        if(newValue == null || newValue == string.Empty)
            return value;

        if(ulong.TryParse(newValue, out var result))
            return result;

        return value;
    }
}
