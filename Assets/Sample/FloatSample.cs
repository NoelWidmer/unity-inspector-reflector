using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class FloatSample : MonoBehaviour
    {
        [SerializeField]
        private float _field;
        [InspectFloat(FloatInspectionType.Field)]
        public float Field
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
        private float _delayedField;
        [InspectFloat(FloatInspectionType.DelayedField)]
        public float DelayedField
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