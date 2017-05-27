using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class CustomizedSample : MonoBehaviour
    {
        [SerializeField]
        private double _delayedDouble;
        [Inspect(InspectionType.DelayedDouble)]
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
        [Inspect(InspectionType.DelayedFloat)]
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
        [Inspect(InspectionType.DelayedString)]
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
        [Inspect(InspectionType.TagString)]
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
        [Inspect(InspectionType.AreaString)]
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