using InspectorReflector;
using UnityEngine;

[EnableIR]
public class UnsignedLongSample : MonoBehaviour
{
    [Inspect]
    public ulong Field;

    [Inspect]
    public ulong Property { get => Field; set => Field = value; }

    [Inspect]
    public ulong ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public ulong PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public ulong PropertyAsSelectable { get => Field; set => Field = value; }
}
