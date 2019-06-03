using InspectorReflector;
using UnityEngine;

[EnableIR]
public class LongSample : MonoBehaviour
{
    [Inspect]
    public long Field;

    [Inspect]
    public long Property { get => Field; set => Field = value; }

    [Inspect]
    public long ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public long PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public long PropertyAsSelectable { get => Field; set => Field = value; }
}
