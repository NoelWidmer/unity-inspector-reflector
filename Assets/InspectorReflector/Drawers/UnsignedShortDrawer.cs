using InspectorReflector;
using InspectorReflector.Implementation;
using UnityEditor;

public class UnsignedShortDrawer : IDrawer<ushort>
{
    public override ushort Draw(IMemberInspectionInfo attr, ushort value)
    {
        int newValue;

        if(attr.InspectAttribute is InspectAsUnsignedShortSliderAttribute attr_)
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

        if(newValue < ushort.MinValue)
            return ushort.MinValue;

        if(newValue > ushort.MaxValue)
            return ushort.MaxValue;

        return (ushort)newValue;
    }
}
