using InspectorReflector;
using UnityEngine;

[EnableIR]
public class UnsignedIntegerSample : MonoBehaviour
{
    [Inspect]
    public uint Field;

    [Inspect]
    public uint Property { get => Field; set => Field = value; }

    [Inspect]
    public uint ReadonlyProperty { get => Field; }
}
