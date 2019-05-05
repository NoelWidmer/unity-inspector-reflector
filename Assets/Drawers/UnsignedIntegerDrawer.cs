using InspectorReflector.Implementation;
using UnityEditor;
using UnityEngine;

public class UnsignedIntegerDrawer : IDrawer<uint>
{
    public override uint Draw(IMemberInspectionInfo attr, uint value)
    {
        long newValue = EditorGUILayout.LongField(attr.Info.Name, value);

        if(newValue < uint.MinValue)
            return uint.MinValue;

        if(newValue > uint.MaxValue)
            return uint.MaxValue;

        return (uint)newValue;
    }
}
