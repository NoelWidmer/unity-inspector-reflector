using UnityEngine;

namespace InspectorReflector.Sample
{
    [EnableIR]
    public class CustomizedSample : MonoBehaviour
    {
        [SerializeField]
        private byte _sliderByte;
        [InspectAsIntSlider(0, 100)]
        public byte SliderByte
        {
            get
            {
                return _sliderByte;
            }
            set
            {
                _sliderByte = value;
            }
        }

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
        [InspectAsFloatSlider(0f, 1f)]
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
        [InspectAsFloatSlider(0f, 1f)]
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
        private int _sliderInt;
        [InspectAsIntSlider(-100, 100)]
        public int SliderInt
        {
            get
            {
                return _sliderInt;
            }
            set
            {
                _sliderInt = value;
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
        private GameObject _dropableObjectAllowSceneObjects;
        [Inspect(InspectionType.DropableObjectAllowSceneObjects)]
        public GameObject DropableObjectAllowSceneObjects
        {
            get
            {
                return _dropableObjectAllowSceneObjects;
            }
            set
            {
                _dropableObjectAllowSceneObjects = value;
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
        private sbyte _sliderSByte;
        [InspectAsIntSlider(-100, 100)]
        public sbyte SliderSByte
        {
            get
            {
                return _sliderSByte;
            }
            set
            {
                _sliderSByte = value;
            }
        }

        [SerializeField]
        private short _sliderShort;
        [InspectAsIntSlider(-100, 100)]
        public short SliderShort
        {
            get
            {
                return _sliderShort;
            }
            set
            {
                _sliderShort = value;
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

        [SerializeField]
        private ushort _sliderUShort;
        [InspectAsIntSlider(0, 100)]
        public ushort SliderUShort
        {
            get
            {
                return _sliderUShort;
            }
            set
            {
                _sliderUShort = value;
            }
        }
    }
}