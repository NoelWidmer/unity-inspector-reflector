using System;

namespace InspectorReflector
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectAttribute : Attribute
    {
    }
}