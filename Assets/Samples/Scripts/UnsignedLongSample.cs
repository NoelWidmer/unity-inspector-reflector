using InspectorReflector;
using UnityEngine;

[EnableIR]
public class UnsignedLongSample : MonoBehaviour
{
    [Inspect]
    public ulong Field;

    [Inspect]
    public ulong Property { get => Field; set => Field = value; }

    [Inspect]
    public ulong ReadonlyProperty { get => Field; }
}
