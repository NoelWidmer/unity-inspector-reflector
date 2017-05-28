using System;

namespace InspectorReflector
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class InspectAsIntSliderAttribute : InspectAttribute
    {
        public readonly int SliderMin;
        public readonly int SliderMax;



        public InspectAsIntSliderAttribute(int sliderMin, int sliderMax)
        {
            SliderMin = sliderMin;
            SliderMax = sliderMax;
        }
    }
}