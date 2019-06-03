using InspectorReflector;
using UnityEngine;

[EnableIR]
public class LayerMaskSample : MonoBehaviour
{
    [Inspect]
    public LayerMask Field;

    [Inspect]
    public LayerMask Property { get => Field; set => Field = value; }

    [Inspect]
    public LayerMask ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public LayerMask PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public LayerMask PropertyAsSelectable { get => Field; set => Field = value; }
}
