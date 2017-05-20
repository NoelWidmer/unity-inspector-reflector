using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class DoubleSample : MonoBehaviour
    {
        [SerializeField]
        private double _field;
        [InspectDouble(DoubleInspectionType.Field)]
        public double Field
        {
            get
            {
                return _field;
            }
            set
            {
                _field = value;
            }
        }

        [SerializeField]
        private double _delayedField;
        [InspectDouble(DoubleInspectionType.DelayedField)]
        public double DelayedField
        {
            get
            {
                return _delayedField;
            }
            set
            {
                _delayedField = value;
            }
        }
    }
}