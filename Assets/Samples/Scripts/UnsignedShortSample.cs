using InspectorReflector;
using UnityEngine;

[EnableIR]
public class UnsignedShortSample : MonoBehaviour
{
    [Inspect]
    public ushort Field;

    [Inspect]
    public ushort Property { get => Field; set => Field = value; }

    [Inspect]
    public ushort ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public ushort PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public ushort PropertyAsSelectable { get => Field; set => Field = value; }
}
