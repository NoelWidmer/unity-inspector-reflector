using System;
using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class SampleObject : MonoBehaviour
    {
        [Inspect]
        public Bounds BoundsProperty { get; set; }
        [Inspect]
        public Color ColorProperty { get; set; }
        [Inspect]
        public AnimationCurve CurveProperty { get; set; }
        [Inspect]
        public double DoubleProperty { get; set; }
        [Inspect]
        public float FloatProperty { get; set; }
        [Inspect]
        public int IntProperty { get; set; }
        [Inspect]
        public Rect RectProperty { get; set; }
        [Inspect]
        public string StringProperty { get; set; }
        [Inspect]
        public bool BoolProperty { get; set; }
        [Inspect]
        public Vector2 Vector2Property { get; set; }
        [Inspect]
        public Vector3 Vector3Property { get; set; }
        [Inspect]
        public Vector4 Vector4Property { get; set; }
        [Inspect]
        public TestEnum TestEnumProperty { get; set; }
        [Inspect]
        public TestFlags TestFlagsProperty { get; set; }
    }



    public enum TestEnum
    {
        One = 1, 
        Two = 2, 
        Three = 3, 
        Four = 4
    }


    [Flags]
    public enum TestFlags
    {
        One = 1,
        Two = 2,
        Four = 4, 
        Eight = 8
    }
}