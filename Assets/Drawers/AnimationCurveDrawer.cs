using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class AnimationCurveDrawer : IDrawer<AnimationCurve>
{
    public override AnimationCurve Draw(IMemberInspectionInfo attr, AnimationCurve value)
    {
        return EditorGUILayout.CurveField(attr.Info.Name, value);
    }
}
