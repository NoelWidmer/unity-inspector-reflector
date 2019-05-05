using System;
using InspectorReflector;
using InspectorReflector.Implementation;
using UnityEditor;

public class DoubleDrawer : IDrawer<double>
{
    public override double Draw(IMemberInspectionInfo attr, double value)
    {
        if(attr.InspectAttribute is InspectAsFloatSliderAttribute attr_)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(attr.Info.Name);

            float narrowedDouble = Convert.ToSingle(value);
            float newValue = EditorGUILayout.Slider(narrowedDouble, attr_.SliderMin, attr_.SliderMax);

            EditorGUILayout.EndHorizontal();
            return newValue;
        }
        else
        {
            switch(attr.InspectAttribute.InspectionKind)
            {
                case InspectionKind.Delayed:
                    return EditorGUILayout.DelayedDoubleField(attr.Info.Name, value);
                default:
                    return EditorGUILayout.DoubleField(attr.Info.Name, value);
            }
        }
    }
}
