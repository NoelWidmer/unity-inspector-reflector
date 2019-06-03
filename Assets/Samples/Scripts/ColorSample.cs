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

    [Inspect(InspectionKind.Immutable)]
    public Color PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public Color PropertyAsSelectable { get => Field; set => Field = value; }
}
