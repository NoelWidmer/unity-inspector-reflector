using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectAttribute : Attribute
    {
        public readonly InspectionType InspectionType = InspectionType.Normal;

        public readonly int? IntSliderMin = null;
        public readonly int? IntSliderMax = null;

        public readonly float? FloatSliderMin = null;
        public readonly float? FloatSliderMax = null;



        public InspectAttribute()
        {
        }

        public InspectAttribute(InspectionType inspectionType)
        {
            InspectionType = inspectionType;
        }

        public InspectAttribute(int sliderMin, int sliderMax)
        {
            IntSliderMin = sliderMin;
            IntSliderMax = sliderMax;
        }

        public InspectAttribute(float sliderMin, float sliderMax)
        {
            FloatSliderMin = sliderMin;
            FloatSliderMax = sliderMax;
        }
    }
}