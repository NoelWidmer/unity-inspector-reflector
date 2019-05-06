using System;

namespace InspectorReflector
{
    /// <summary>
    ///     Properties and Fields marked with this attribute can be inspected by the IR.<br>
    ///     If the member is of type <see cref="ushort"/> the IR will display a dragable slider.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class InspectAsUnsignedShortSliderAttribute : InspectAttribute
    {
        public readonly ushort SliderMin;
        public readonly ushort SliderMax;



        /// <summary/>
        /// <param name="sliderMin">The minimum value of the slider.</param>
        /// <param name="sliderMax">The maximum value of the slider.</param>
        public InspectAsUnsignedShortSliderAttribute(ushort sliderMin, ushort sliderMax)
        {
            SliderMin = sliderMin;
            SliderMax = sliderMax;
        }
    }
}