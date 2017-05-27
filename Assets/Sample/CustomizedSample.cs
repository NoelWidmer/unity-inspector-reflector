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
        private double _sliderDouble;
        [Inspect(0f, 1f)]
        public double SliderDouble
        {
            get
            {
                return _sliderDouble;
            }
            set
            {
                _sliderDouble = value;
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
        private float _sliderFloat;
        [Inspect(0f, 1f)]
        public float SliderFloat
        {
            get
            {
                return _sliderFloat;
            }
            set
            {
                _sliderFloat = value;
            }
        }

        [SerializeField]
        private int _delayedInt;
        [Inspect(InspectionType.DelayedInt)]
        public int DelayedInt
        {
            get
            {
                return _delayedInt;
            }
            set
            {
                _delayedInt = value;
            }
        }

        [SerializeField]
        private GameObject _dropableObject;
        [Inspect(InspectionType.DropableObject)]
        public GameObject DropableObject
        {
            get
            {
                return _dropableObject;
            }
            set
            {
                _dropableObject = value;
            }
        }

        [SerializeField]
        private Rect _rectReadonlyByIR;
        [Inspect(InspectionType.Readonly)]
        public Rect RectReadonlyByIR
        {
            get
            {
                return _rectReadonlyByIR;
            }
            set
            {
                _rectReadonlyByIR = value;
            }
        }

        [SerializeField]
        private Rect _rectReadonlySelectableByIR;
        [Inspect(InspectionType.ReadonlySelectable)]
        public Rect RectReadonlySelectableByIR
        {
            get
            {
                return _rectReadonlySelectableByIR;
            }
            set
            {
                _rectReadonlySelectableByIR = value;
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