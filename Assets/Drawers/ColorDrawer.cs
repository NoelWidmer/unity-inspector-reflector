using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class ColorDrawer : IDrawer<Color>
{
    public override Color Draw(IMemberInspectionInfo attr, Color value)
    {
        return EditorGUILayout.ColorField(attr.Info.Name, value);
    }
}
