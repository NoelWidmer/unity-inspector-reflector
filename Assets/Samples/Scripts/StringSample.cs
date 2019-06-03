using InspectorReflector;
using UnityEngine;

[EnableIR]
public class StringSample : MonoBehaviour
{
    [Inspect]
    public string Field = "enjoy :)";

    [Inspect]
    public string Property { get => Field; set => Field = value; }

    [Inspect]
    public string ReadonlyProperty { get => Field; }

    [Inspect(InspectionKind.Immutable)]
    public string PropertyAsReadonly { get => Field; set => Field = value; }

    [Inspect(InspectionKind.ImmutableSelectable)]
    public string PropertyAsSelectable { get => Field; set => Field = value; }

    [Inspect(InspectionKind.Tag)]
    public string Tag { get => Field; set => Field = value; }

    [Inspect(InspectionKind.TextArea)]
    public string Text { get => Field; set => Field = value; }
}
