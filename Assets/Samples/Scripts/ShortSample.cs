using InspectorReflector;
using UnityEngine;

[EnableIR]
public class ShortSample : MonoBehaviour
{
    [Inspect]
    public short Field;

    [Inspect]
    public short Property { get => Field; set => Field = value; }

    [Inspect]
    public short ReadonlyProperty { get => Field; }
}
