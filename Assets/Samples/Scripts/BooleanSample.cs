using InspectorReflector;
using UnityEngine;

[EnableIR]
public class BooleanSample : MonoBehaviour
{
    [Inspect]
    public bool BooleanField;

    [Inspect]
    public bool BooleanProperty { get => BooleanField; set => BooleanField = value; }

    [Inspect]
    public bool BooleanReadonlyProperty { get => BooleanField; }
}
