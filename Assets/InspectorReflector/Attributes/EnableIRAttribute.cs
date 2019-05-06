using System;

namespace InspectorReflector
{
    /// <summary>
    ///     Classes marked with this attribute will be drawn using the IR.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EnableIRAttribute : Attribute
    {
    }
}