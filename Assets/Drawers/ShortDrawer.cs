using InspectorReflector;
using InspectorReflector.Implementation;
using UnityEditor;

public class ShortDrawer : IDrawer<short>
{
    public override short Draw(IMemberInspectionInfo attr, short value)
    {
        int newValue;

        if(attr.InspectAttribute is InspectAsShortSliderAttribute attr_)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(attr.Info.Name);
            newValue = EditorGUILayout.IntSlider(value, attr_.SliderMin, attr_.SliderMax);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            newValue = EditorGUILayout.IntField(attr.Info.Name, value);
        }

        if(newValue < short.MinValue)
            return short.MinValue;

        if(newValue > short.MaxValue)
            return short.MaxValue;

        return (short)newValue;
    }
}
