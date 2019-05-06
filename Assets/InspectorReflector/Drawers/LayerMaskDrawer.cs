using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class LayerMaskDrawer : IDrawer<LayerMask>
{
    public override LayerMask Draw(IMemberInspectionInfo attr, LayerMask value)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel(attr.Info.Name);
        int newValue = EditorGUILayout.LayerField(value);
        EditorGUILayout.EndHorizontal();
        return newValue;
    }
}
