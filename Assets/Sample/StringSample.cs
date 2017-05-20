 using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class StringSample : MonoBehaviour
    {
        [SerializeField]
        private string _field;
        [InspectString(StringInspectionType.Field)]
        public string Field
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
        private string _delayedField;
        [InspectString(StringInspectionType.DelayedField)]
        public string DelayedField
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

        [SerializeField]
        private string _tag;
        [InspectString(StringInspectionType.Tag)]
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        [SerializeField]
        private string _area;
        [InspectString(StringInspectionType.Area)]
        public string Area
        {
            get
            {
                return _area;
            }
            set
            {
                _area = value;
            }
        }
    }
}