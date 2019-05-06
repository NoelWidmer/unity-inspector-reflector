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
}
