using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class LongDrawer : IDrawer<long>
{
    public override long Draw(IMemberInspectionInfo attr, long value)
    {
        return EditorGUILayout.LongField(attr.Info.Name, value);
    }
}
