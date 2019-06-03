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

    [Inspect(InspectionKind.Immutable)]
    public byte PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public byte PropertyAsSelectable { get => Field; set => Field = value; }
}
