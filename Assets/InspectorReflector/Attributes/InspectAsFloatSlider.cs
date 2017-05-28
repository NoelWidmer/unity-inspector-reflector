using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectAsFloatSliderAttribute : InspectAttribute
    {
        public readonly float SliderMin;
        public readonly float SliderMax;



        public InspectAsFloatSliderAttribute(float sliderMin, float sliderMax)
        {
            SliderMin = sliderMin;
            SliderMax = sliderMax;
        }
    }
}