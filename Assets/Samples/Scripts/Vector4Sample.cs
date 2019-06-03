using InspectorReflector;
using UnityEngine;

[EnableIR]
public class Vector4Sample : MonoBehaviour
{
    [Inspect]
    public Vector4 Field;

    [Inspect]
    public Vector4 Property { get => Field; set => Field = value; }

    [Inspect]
    public Vector4 ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public Vector4 PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public Vector4 PropertyAsSelectable { get => Field; set => Field = value; }
}
