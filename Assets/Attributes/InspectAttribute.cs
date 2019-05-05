using System;

namespace InspectorReflector
{
    /// <summary>
    ///     Properties and Fields marked with this attribute can be inspected by the IR.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectAttribute : Attribute
    {
        public readonly InspectionKind InspectionKind;



        /// <summary>
        ///     Sets the <see cref="InspectionKind"/> to <see cref="InspectionKind.DefaultIR"/>.
        /// </summary>
        public InspectAttribute()
        {
            InspectionKind = InspectionKind.DefaultIR;
        }

        /// <summary>
        ///     Allows a custom <see cref="InspectionKind"/>.
        /// </summary>
        public InspectAttribute(InspectionKind inspectionKind)
        {
            InspectionKind = inspectionKind;
        }
    }
}