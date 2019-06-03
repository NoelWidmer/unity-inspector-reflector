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

    [Inspect(InspectionKind.Immutable)]
    public uint PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public uint PropertyAsSelectable { get => Field; set => Field = value; }
}
