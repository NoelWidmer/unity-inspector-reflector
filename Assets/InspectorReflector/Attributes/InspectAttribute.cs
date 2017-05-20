using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectAttribute : Attribute
    {
        private bool _inspectAsReadonly = false;

        public InspectAttribute()
        {
        }

        public InspectAttribute(bool inspectAsReadonly)
        {
            _inspectAsReadonly = inspectAsReadonly;
        }

        public bool InspectAsReadonly
        {
            get
            {
                return _inspectAsReadonly;
            }
        }
    }
}