using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class DoubleSample : MonoBehaviour
    {
        [InspectDouble(DoubleInspectionType.Field)]
        public double Field
        {
            get; set;
        }

        [InspectDouble(DoubleInspectionType.DelayedField)]
        public double DelayedField
        {
            get; set;
        }
    }
}