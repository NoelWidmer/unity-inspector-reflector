using InspectorReflector;
using InspectorReflector.Implementation;
using UnityEditor;

public class SignedByteDrawer : IDrawer<sbyte>
{
    public override sbyte Draw(IMemberInspectionInfo attr, sbyte value)
    {
        int newValue;

        if(attr.InspectAttribute is InspectAsSignedByteSliderAttributeAttribute attr_)
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

        if(newValue < sbyte.MinValue)
            return sbyte.MinValue;

        if(newValue > sbyte.MaxValue)
            return sbyte.MaxValue;

        return (sbyte)newValue;
    }
}
