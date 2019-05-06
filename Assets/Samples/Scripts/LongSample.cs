using InspectorReflector;
using UnityEngine;

[EnableIR]
public class LongSample : MonoBehaviour
{
    [Inspect]
    public long Field;

    [Inspect]
    public long Property { get => Field; set => Field = value; }

    [Inspect]
    public long ReadonlyProperty { get => Field; }
}
