using InspectorReflector;
using UnityEngine;

[EnableIR]
public class UnityObjectWithSceneSample : MonoBehaviour
{
    public UnityObjectWithSceneSample()
    {
        Field = transform;
    }

    [Inspect(InspectionKind.DropableObjectAllowSceneObjects)]
    public Object Field;

    [Inspect(InspectionKind.DropableObjectAllowSceneObjects)]
    public Object Property { get => Field; set => Field = value; }
}
