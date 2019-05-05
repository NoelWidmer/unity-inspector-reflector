using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class Vector2Drawer : IDrawer<Vector2>
{
    public override Vector2 Draw(IMemberInspectionInfo attr, Vector2 value)
    {
        return EditorGUILayout.Vector2Field(attr.Info.Name, value);
    }
}
