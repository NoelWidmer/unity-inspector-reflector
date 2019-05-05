using InspectorReflector.Implementation;
using UnityEditor;

public class BooleanDrawer : IDrawer<bool>
{
    public override bool Draw(IMemberInspectionInfo attr, bool value)
    {
        return EditorGUILayout.Toggle(attr.Info.Name, value);
    }
}
