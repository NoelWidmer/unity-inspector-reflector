using System;

namespace InspectorReflector
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class InspectAsByteSliderAttribute : InspectAttribute
    {
        public readonly byte SliderMin;
        public readonly byte SliderMax;



        public InspectAsByteSliderAttribute(byte sliderMin, byte sliderMax)
        {
            SliderMin = sliderMin;
            SliderMax = sliderMax;
        }
    }
}