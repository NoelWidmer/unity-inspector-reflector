using System;

namespace InspectorReflector
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectAttribute : Attribute
    {
        public readonly InspectionType InspectionType = InspectionType.Normal;



        public InspectAttribute()
        {
        }

        public InspectAttribute(InspectionType inspectionType)
        {
            InspectionType = inspectionType;
        }
    }
}