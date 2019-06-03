using InspectorReflector;
using UnityEngine;

[EnableIR]
public class SignedByteSample : MonoBehaviour
{
    [Inspect]
    public sbyte Field;

    [Inspect]
    public sbyte Property { get => Field; set => Field = value; }

    [Inspect]
    public sbyte ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public sbyte PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public sbyte PropertyAsSelectable { get => Field; set => Field = value; }
}
