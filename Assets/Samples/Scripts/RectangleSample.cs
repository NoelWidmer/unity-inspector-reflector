using InspectorReflector;
using UnityEngine;

[EnableIR]
public class RectangleSample : MonoBehaviour
{
    [Inspect]
    public Rect Field;

    [Inspect]
    public Rect Property { get => Field; set => Field = value; }

    [Inspect]
    public Rect ReadonlyProperty { get => Field; }
}
