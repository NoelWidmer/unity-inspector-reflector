using InspectorReflector;
using UnityEngine;

[EnableIR]
public class FloatSample : MonoBehaviour
{
    [Inspect]
    public float Field;

    [Inspect]
    public float Property { get => Field; set => Field = value; }

    [Inspect]
    public float ReadonlyProperty { get => Field; }
}
