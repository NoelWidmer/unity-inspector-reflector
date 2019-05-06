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
}
