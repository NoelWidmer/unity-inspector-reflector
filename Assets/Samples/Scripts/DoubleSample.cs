using InspectorReflector;
using UnityEngine;

[EnableIR]
public class DoubleSample : MonoBehaviour
{
    [Inspect]
    public double Field;

    [Inspect]
    public double Property { get => Field; set => Field = value; }

    [Inspect]
    public double ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public double PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public double PropertyAsSelectable { get => Field; set => Field = value; }
}
