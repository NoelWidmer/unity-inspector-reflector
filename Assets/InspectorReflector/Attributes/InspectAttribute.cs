using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectAttribute : Attribute
    {
        private readonly InspectionType _inspectionType = InspectionType.Normal;



        public InspectAttribute()
        {
        }

        public InspectAttribute(InspectionType inspectionType)
        {
            _inspectionType = inspectionType;
        }



        public InspectionType InspectionType
        {
            get
            {
                return _inspectionType;
            }
        }
    }
}