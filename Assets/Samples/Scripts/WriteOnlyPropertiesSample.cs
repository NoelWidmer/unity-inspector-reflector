using InspectorReflector;
using UnityEngine;

[EnableIR]
public class WriteOnlyPropertiesSample : MonoBehaviour
{
    [Inspect]
    public byte BytePrivateSetter
    {
        private get => default;
        set { }
    }

    [Inspect]
    public byte ByteNoSetter
    {
        set { }
    }
}
