using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class BoundsDrawer : IDrawer<Bounds>
{
    public override Bounds Draw(IMemberInspectionInfo attr, Bounds value)
    {
        return EditorGUILayout.BoundsField(attr.Info.Name, value);
    }
}
