using InspectorReflector;
using UnityEngine;

[EnableIR]
public class LayerMaskSample : MonoBehaviour
{
    [Inspect]
    public LayerMask Field;

    [Inspect]
    public LayerMask Property { get => Field; set => Field = value; }

    [Inspect]
    public LayerMask ReadonlyProperty { get => Field; }
}
