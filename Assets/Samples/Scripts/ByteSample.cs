using InspectorReflector;
using UnityEngine;

[EnableIR]
public class ByteSample : MonoBehaviour
{
    [Inspect]
    public byte Field;

    [Inspect]
    public byte Property { get => Field; set => Field = value; }

    [Inspect]
    public byte ReadonlyProperty { get => Field; }
}
