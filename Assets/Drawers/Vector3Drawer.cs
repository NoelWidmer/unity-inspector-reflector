using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class Vector3Drawer : IDrawer<Vector3>
{
    public override Vector3 Draw(IMemberInspectionInfo attr, Vector3 value)
    {
        return EditorGUILayout.Vector3Field(attr.Info.Name, value);
    }
}
