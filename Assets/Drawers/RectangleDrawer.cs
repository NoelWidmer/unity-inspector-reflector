using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class RectangleDrawer : IDrawer<Rect>
{
    public override Rect Draw(IMemberInspectionInfo attr, Rect value)
    {
        return EditorGUILayout.RectField(attr.Info.Name, value);
    }
}
