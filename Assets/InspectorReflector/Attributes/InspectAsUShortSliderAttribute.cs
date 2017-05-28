using System;

namespace InspectorReflector
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class InspectAsUShortSliderAttribute : InspectAttribute
    {
        public readonly ushort SliderMin;
        public readonly ushort SliderMax;



        public InspectAsUShortSliderAttribute(ushort sliderMin, ushort sliderMax)
        {
            SliderMin = sliderMin;
            SliderMax = sliderMax;
        }
    }
}