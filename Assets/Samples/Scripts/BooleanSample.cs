using InspectorReflector;
using UnityEngine;

[EnableIR]
public class BooleanSample : MonoBehaviour
{
    [Inspect]
    public bool Field;

    [Inspect]
    public bool Property { get => Field; set => Field = value; }

    [Inspect]
    public bool ReadonlyProperty { get => Field; }
}
