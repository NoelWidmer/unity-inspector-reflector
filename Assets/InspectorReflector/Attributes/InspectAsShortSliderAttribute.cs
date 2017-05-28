using System;

namespace InspectorReflector
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class InspectAsShortSliderAttribute : InspectAttribute
    {
        public readonly short SliderMin;
        public readonly short SliderMax;



        public InspectAsShortSliderAttribute(short sliderMin, short sliderMax)
        {
            SliderMin = sliderMin;
            SliderMax = sliderMax;
        }
    }
}