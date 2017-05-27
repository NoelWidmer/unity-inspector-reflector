using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class CustomizedSample : MonoBehaviour
    {
        [SerializeField]
        private double _delayedDouble;
        [InspectDouble(DoubleInspectionType.Delayed)]
        public double DelayedDouble
        {
            get
            {
                return _delayedDouble;
            }
            set
            {
                _delayedDouble = value;
            }
        }

        [SerializeField]
        private float _delayedFloat;
        [InspectFloat(FloatInspectionType.Delayed)]
        public float DelayedFloat
        {
            get
            {
                return _delayedFloat;
            }
            set
            {
                _delayedFloat = value;
            }
        }

        [SerializeField]
        private string _delayedString;
        [InspectString(StringInspectionType.Delayed)]
        public string DelayedString
        {
            get
            {
                return _delayedString;
            }
            set
            {
                _delayedString = value;
            }
        }

        [SerializeField]
        private string _tagString;
        [InspectString(StringInspectionType.Tag)]
        public string TagString
        {
            get
            {
                return _tagString;
            }
            set
            {
                _tagString = value;
            }
        }

        [SerializeField]
        private string _areaString;
        [InspectString(StringInspectionType.Area)]
        public string AreaString
        {
            get
            {
                return _areaString;
            }
            set
            {
                _areaString = value;
            }
        }
    }
}