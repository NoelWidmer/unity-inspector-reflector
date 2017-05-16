using UnityEngine;

namespace InspectorReflector.Sample
{
    [Inspect]
    public class SampleObject : MonoBehaviour
    {
        [Inspect]
        public int IntProperty
        {
            get; set;
        }



        [Inspect]
        public int ReadonlyProperty
        {
            get
            {
                return 3;
            }
        }


        [Inspect]
        public int WriteonlyProperty
        {
            set
            {
                
            }
        }
    }
}