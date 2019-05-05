using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class Vector4Drawer : IDrawer<Vector4>
{
    public override Vector4 Draw(IMemberInspectionInfo attr, Vector4 value)
    {
        return EditorGUILayout.Vector4Field(attr.Info.Name, value);
    }
}
