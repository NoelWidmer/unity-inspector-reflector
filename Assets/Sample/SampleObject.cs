using InspectorReflector;
using UnityEngine;

public class SampleObject : MonoBehaviour
{
    [Inspect]
    public int IntField;
    
    [Inspect]
    public int IntProperty { get; set; }
}