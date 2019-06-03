using InspectorReflector;
using UnityEngine;

[EnableIR]
public class UnityObjectWithoutSceneSample : MonoBehaviour
{
    [Inspect(InspectionKind.DropableObject)]
    public Object Field;

    [Inspect(InspectionKind.DropableObject)]
    public Object Property { get => Field; set => Field = value; }
}
