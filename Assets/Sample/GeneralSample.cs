using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class GeneralSample : MonoBehaviour
    {
        [SerializeField]
        private Bounds _boundsProperty;
        [Inspect]
        public Bounds BoundsProperty {
            get
            {
                return _boundsProperty;
            }
            set
            {
                _boundsProperty = value;
            }
        }



        [SerializeField]
        private Color _colorProperty;
        [Inspect]
        public Color ColorProperty { get
            {
                return _colorProperty;
            }
            set
            {
                _colorProperty = value;
            }
        }



        [SerializeField]
        private AnimationCurve _curveProperty;
        [Inspect]
        public AnimationCurve CurveProperty {
            get
            {
                return _curveProperty;
            }
            set
            {
                _curveProperty = value;
            }
        }



        [SerializeField]
        private int _intProperty;
        [Inspect]
        public int IntProperty {
            get
            {
                return _intProperty;
            }
            set
            {
                _intProperty = value;
            }
        }



        [SerializeField]
        private long _longProperty;
        [Inspect]
        public long LongProperty
        {
            get
            {
                return _longProperty;
            }
            set
            {
                _longProperty = value;
            }
        }



        [SerializeField]
        private Rect _rectProperty;
        [Inspect]
        public Rect RectProperty
        {
            get
            {
                return _rectProperty;
            }
            set
            {
                _rectProperty = value;
            }
        }



        [SerializeField]
        private Rect rectPropertyReadonly;
        [Inspect(true)]
        public Rect RectPropertyReadonly
        {
            get
            {
                return rectPropertyReadonly;
            }
            set
            {
                rectPropertyReadonly = value;
            }
        }



        [SerializeField]
        private bool _boolProperty;
        [Inspect]
        public bool BoolProperty
        {
            get
            {
                return _boolProperty;
            }
            set
            {
                _boolProperty = value;
            }
        }



        [SerializeField]
        private Vector2 _vector2Property;
        [Inspect]
        public Vector2 Vector2Property
        {
            get
            {
                return _vector2Property;
            }
            set
            {
                _vector2Property = value;
            }
        }



        [SerializeField]
        private Vector3 _vector3Property;
        [Inspect]
        public Vector3 Vector3Property
        {
            get
            {
                return _vector3Property;
            }
            set
            {
                _vector3Property = value;
            }
        }



        [SerializeField]
        private Vector4 _vector4Property;
        [Inspect]
        public Vector4 Vector4Property
        {
            get
            {
                return _vector4Property;
            }
            set
            {
                _vector4Property = value;
            }
        }
    }
}