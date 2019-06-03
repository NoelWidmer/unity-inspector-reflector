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

    [Inspect(InspectionKind.Immutable)]
    public short PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public short PropertyAsSelectable { get => Field; set => Field = value; }
}
