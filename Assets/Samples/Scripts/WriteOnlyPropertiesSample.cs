using InspectorReflector;
using UnityEngine;

[EnableIR]
public class WriteOnlyPropertiesSample : MonoBehaviour
{
    [Inspect]
    public byte PrivateSetter
    {
        private get => default;
        set { }
    }

    [Inspect]
    public byte NoSetter
    {
        set { }
    }
}
