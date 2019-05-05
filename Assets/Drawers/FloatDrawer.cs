using InspectorReflector;
using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class FloatDrawer : IDrawer<float>
{
    public override float Draw(IMemberInspectionInfo attr, float value)
    {
        if(attr.InspectAttribute is InspectAsFloatSliderAttribute attr_)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(attr.Info.Name);
            var newVal = EditorGUILayout.Slider(value, attr_.SliderMin, attr_.SliderMax);
            EditorGUILayout.EndHorizontal();
            return newVal;
        }
        else
        {
            switch(attr.InspectAttribute.InspectionKind)
            {
                case InspectionKind.Delayed:
                    return EditorGUILayout.DelayedFloatField(attr.Info.Name, value);
                default:
                    return EditorGUILayout.FloatField(attr.Info.Name, value);
            }
        }
    }
}
