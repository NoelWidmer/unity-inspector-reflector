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
}
