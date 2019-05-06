using InspectorReflector;
using InspectorReflector.Implementation;
using UnityEditor;

public class StringDrawer : IDrawer<string>
{
    public override string Draw(IMemberInspectionInfo attr, string value)
    {
        switch(attr.InspectAttribute.InspectionKind)
        {
            case InspectionKind.Delayed:
                return EditorGUILayout.DelayedTextField(attr.Info.Name, value);

            case InspectionKind.Tag:
                return EditorGUILayout.TagField(attr.Info.Name, value);

            case InspectionKind.TextArea:
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel(attr.Info.Name);
                var result = EditorGUILayout.TextArea(value);
                EditorGUILayout.EndHorizontal();
                return result;

            default:
                return EditorGUILayout.TextField(attr.Info.Name, value);
        }
    }
}
