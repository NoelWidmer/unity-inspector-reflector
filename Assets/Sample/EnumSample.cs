using System;
using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class EnumSample : MonoBehaviour
    {
        [SerializeField]
        private TestEnum _testEnumProperty;
        [Inspect]
        public TestEnum TestEnumProperty
        {
            get
            {
                return _testEnumProperty;
            }
            set
            {
                _testEnumProperty = value;
            }
        }

        [SerializeField]
        private TestFlags _testFlagsProperty;
        [Inspect]
        public TestFlags TestFlagsProperty
        {
            get
            {
                return _testFlagsProperty;
            }
            set
            {
                _testFlagsProperty = value;
            }
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