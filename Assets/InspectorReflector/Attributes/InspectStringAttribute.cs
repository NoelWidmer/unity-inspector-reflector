using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectStringAttribute : InspectAttribute
    {
        private readonly StringInspectionType _inspectionType;

        public InspectStringAttribute(StringInspectionType type)
        {
            _inspectionType = type;
        }

        public StringInspectionType InspectionType
        {
            get
            {
                return _inspectionType;
            }
        }
    }



    public enum StringInspectionType
    {
        Default, 
        Delayed, 
        Tag, 
        Area
    }
}