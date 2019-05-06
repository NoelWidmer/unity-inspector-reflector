using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class DropableObjectDrawer
{
    public Object Draw(IMemberInspectionInfo attr, Object value, bool allowSceneObjects)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel(attr.Info.Name);
        var result = EditorGUILayout.ObjectField(value, attr.RealType, allowSceneObjects);
        EditorGUILayout.EndHorizontal();
        return result;
    }
}
