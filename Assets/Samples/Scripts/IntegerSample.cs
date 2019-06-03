using InspectorReflector;
using UnityEngine;

[EnableIR]
public class IntegerSample : MonoBehaviour
{
    [Inspect]
    public int Field;

    [Inspect]
    public int Property { get => Field; set => Field = value; }

    [Inspect]
    public int ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public int PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public int PropertyAsSelectable { get => Field; set => Field = value; }
}
