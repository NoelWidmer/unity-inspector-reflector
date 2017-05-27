using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectFloatAttribute : InspectAttribute
    {
        private readonly FloatInspectionType _inspectionType;

        public InspectFloatAttribute(FloatInspectionType type)
        {
            _inspectionType = type;
        }

        public FloatInspectionType InspectionType
        {
            get
            {
                return _inspectionType;
            }
        }
    }



    public enum FloatInspectionType
    {
        Default,
        Delayed
    }
}