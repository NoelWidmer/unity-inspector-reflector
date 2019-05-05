using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class CharacterDrawer : IDrawer<char>
{
    public override char Draw(IMemberInspectionInfo attr, char value)
    {
        string newValue = EditorGUILayout.TextField(attr.Info.Name, value.ToString());

        if(newValue == null || newValue == string.Empty)
            return value;

        return newValue[0];
    }
}
