using InspectorReflector;
using InspectorReflector.Implementation;
using UnityEditor;

public class ByteDrawer : IDrawer<byte>
{
    public override byte Draw(IMemberInspectionInfo attr, byte value)
    {
        int newValue;

        if(attr.InspectAttribute is InspectAsByteSliderAttribute attr_)
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

        if(newValue < byte.MinValue)
            return byte.MinValue;

        if(newValue > byte.MaxValue)
            return byte.MaxValue;

        return (byte)newValue;
    }
}
