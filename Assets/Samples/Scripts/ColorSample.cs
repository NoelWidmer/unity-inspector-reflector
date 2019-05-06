using InspectorReflector;
using UnityEngine;

[EnableIR]
public class ColorSample : MonoBehaviour
{
    [Inspect]
    public Color Field;

    [Inspect]
    public Color Property { get => Field; set => Field = value; }

    [Inspect]
    public Color ReadonlyProperty { get => Field; }
}
