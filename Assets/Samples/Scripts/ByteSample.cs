using InspectorReflector;
using UnityEngine;

[EnableIR]
public class ByteSample : MonoBehaviour
{
    [Inspect]
    public byte ByteField;

    [Inspect]
    public byte ByteProperty { get => ByteField; set => ByteField = value; }

    [Inspect]
    public byte ByteReadonlyProperty { get => ByteField; }
}
