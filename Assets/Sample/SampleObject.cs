using UnityEngine;

namespace InspectorReflector.Sample
{
    public class SampleObject : MonoBehaviour, IInspectable
    {
        [Inspect]
        public int IntField;

        [Inspect]
        public int IntProperty
        {
            get; set;
        }
    }
}