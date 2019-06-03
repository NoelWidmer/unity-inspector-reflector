using InspectorReflector;
using UnityEngine;

[EnableIR]
public class Vector2Sample : MonoBehaviour
{
    [Inspect]
    public Vector2 Field;

    [Inspect]
    public Vector2 Property { get => Field; set => Field = value; }

    [Inspect]
    public Vector2 ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public Vector2 PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public Vector2 PropertyAsSelectable { get => Field; set => Field = value; }
}
