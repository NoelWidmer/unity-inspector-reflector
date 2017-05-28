using UnityEngine;

namespace InspectorReflector.Sample
{
    [EnableIR]
    public class CustomizedSample : MonoBehaviour
    {
        [InspectAsByteSlider(0, 100)]
        public byte SliderByte;

        [Inspect(InspectionType.DelayedDouble)]
        public double DelayedDouble;

        [InspectAsFloatSlider(0f, 1f)]
        public double SliderDouble;

        [Inspect(InspectionType.DelayedFloat)]
        public float DelayedFloat;

        [InspectAsFloatSlider(0f, 1f)]
        public float SliderFloat;

        [Inspect(InspectionType.DelayedInt)]
        public int DelayedInt;

        [InspectAsIntSlider(-100, 100)]
        public int SliderInt;

        [Inspect(InspectionType.DropableObject)]
        public GameObject DropableObject;

        [Inspect(InspectionType.DropableObjectAllowSceneObjects)]
        public GameObject DropableObjectAllowSceneObjects;

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

        [InspectAsSByteSlider(-100, 100)]
        public sbyte SliderSByte;

        [InspectAsShortSlider(-100, 100)]
        public short SliderShort;

        [Inspect(InspectionType.DelayedString)]
        public string DelayedString;

        [Inspect(InspectionType.TagString)]
        public string TagString;

        [Inspect(InspectionType.AreaString)]
        public string AreaString;

        [InspectAsUShortSlider(0, 100)]
        public ushort SliderUShort;
    }
}