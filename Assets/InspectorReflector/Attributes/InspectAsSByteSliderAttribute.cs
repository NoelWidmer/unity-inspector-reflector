using System;

namespace InspectorReflector
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class InspectAsSByteSliderAttribute : InspectAttribute
    {
        public readonly sbyte SliderMin;
        public readonly sbyte SliderMax;



        public InspectAsSByteSliderAttribute(sbyte sliderMin, sbyte sliderMax)
        {
            SliderMin = sliderMin;
            SliderMax = sliderMax;
        }
    }
}