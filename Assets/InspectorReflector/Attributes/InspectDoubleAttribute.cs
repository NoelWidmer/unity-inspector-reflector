
using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectDoubleAttribute : InspectAttribute
    {
        private readonly DoubleInspectionType _inspectionType;

        public InspectDoubleAttribute(DoubleInspectionType type)
        {
            _inspectionType = type;
        }

        public DoubleInspectionType InspectionType
        {
            get
            {
                return _inspectionType;
            }
        }
    }



    public enum DoubleInspectionType
    {
        Default,
        Delayed
    }
}