using InspectorReflector;
using UnityEngine;

[EnableIR]
public class BoundsSample : MonoBehaviour
{
    [Inspect]
    public Bounds Field;

    [Inspect]
    public Bounds Property { get => Field; set => Field = value; }

    [Inspect]
    public Bounds ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public Bounds PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public Bounds PropertyAsSelectable { get => Field; set => Field = value; }
}
