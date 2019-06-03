using InspectorReflector;
using UnityEngine;

[EnableIR]
public class Vector3Sample : MonoBehaviour
{
    [Inspect]
    public Vector3 Field;

    [Inspect]
    public Vector3 Property { get => Field; set => Field = value; }

    [Inspect]
    public Vector3 ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public Vector3 PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public Vector3 PropertyAsSelectable { get => Field; set => Field = value; }
}
