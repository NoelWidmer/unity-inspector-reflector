using InspectorReflector;
using InspectorReflector.Implementation;
using UnityEditor;

public class IntegerDrawer : IDrawer<int>
{
    public override int Draw(IMemberInspectionInfo attr, int value)
    {
        if(attr.InspectAttribute is InspectAsIntegerSliderAttributeAttribute attr_)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(attr.Info.Name);
            var newVal = EditorGUILayout.IntSlider(value, attr_.SliderMin, attr_.SliderMax);
            EditorGUILayout.EndHorizontal();
            return newVal;
        }
        else
        {
            switch(attr.InspectAttribute.InspectionKind)
            {
                case InspectionKind.Delayed:
                    return EditorGUILayout.DelayedIntField(attr.Info.Name, value);
                default:
                    return EditorGUILayout.IntField(attr.Info.Name, value);
            }
        }
    }
}
