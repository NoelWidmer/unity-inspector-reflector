using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class FloatSample : MonoBehaviour
    {
        [InspectFloat(FloatInspectionType.Field)]
        public float Field
        {
            get; set;
        }

        [InspectFloat(FloatInspectionType.DelayedField)]
        public float DelayedField
        {
            get; set;
        }
    }
}