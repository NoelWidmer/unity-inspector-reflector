using System;
using UnityEngine;

namespace InspectorReflector.Sample
{
    [EnableIR]
    public class DefaultSample : MonoBehaviour
    {
        // Default

        // This is implemented as a property to show that properties work as well.
        [SerializeField]
        private AnimationCurve _defaultAnimationCurve;
        [Inspect]
        public AnimationCurve DefaultAnimationCurve
        {
            get
            {
                return _defaultAnimationCurve;
            }
            set
            {
                _defaultAnimationCurve = value;
            }
        }



        [Inspect]
        public bool DefaultBool;

        [Inspect]
        public byte DefaultByte;
        
        [Inspect]
        public Bounds DefaultBounds;

        [Inspect]
        public char DefaultChar;

        [Inspect]
        public Color DefaultColor;

        [Inspect]
        public double DefaultDouble;

        [Inspect]
        public Enum DefaultEnum;

        [Inspect]
        public float DefaultFloat;

        [Inspect]
        public int DefaultInt;

        [Inspect]
        public LayerMask DefaultLayerMask;

        [Inspect]
        public long DefaultLong;

        [Inspect]
        public ObjectSample DefaultObjectSample = new ObjectSample();

        [Inspect]
        public Rect DefaultRect;

        [Inspect]
        public sbyte DefaultSByte;

        [Inspect]
        public short DefaultShort;
        
        [Inspect]
        public string DefaultString;
        
        [Inspect]
        public uint DefaultUInt;

        [Inspect]
        public ulong DefaultULong;

        [Inspect]
        public ushort DefaultUShort;

        [Inspect]
        public Vector2 DefaultVector2;

        [Inspect]
        public Vector3 DefaultVector3;

        [Inspect]
        public Vector4 DefaultVector4;



        // TODO dispaly a tile

        [Inspect]
        public Flags DefaultFlagEnum;

        [SerializeField]
        private Rect _defaultRectReadonlyByCode;
        [Inspect]
        public Rect DefaultRectReadonlyByCode
        {
            get
            {
                return _defaultRectReadonlyByCode;
            }
        }



        // Nested types.

        public enum Enum
        {
            Zero = 0, 
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4
        }




        [Flags]
        public enum Flags
        {
            One = 1,
            Two = 2,
            Four = 4,
            Eight = 8
        }



        public class ObjectSample
        {
            [Inspect]
            public string Name;
            
            [SerializeField]
            private int _exp;
            [Inspect]
            public int EXP
            {
                get
                {
                    return _exp;
                }
                set
                {
                    _exp = value;
                    _level = (int)Mathf.Floor(value / 100f);
                }
            }
            
            [SerializeField]
            private int _level;
            [Inspect]
            public int Level
            {
                get
                {
                    return _level;
                }
            }
        }
    }
}