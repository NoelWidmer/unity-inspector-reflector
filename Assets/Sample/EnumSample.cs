using System;
using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class EnumSample : MonoBehaviour
    {
        [Inspect]
        public TestEnum TestEnumProperty
        {
            get; set;
        }

        [Inspect]
        public TestFlags TestFlagsProperty
        {
            get; set;
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
}