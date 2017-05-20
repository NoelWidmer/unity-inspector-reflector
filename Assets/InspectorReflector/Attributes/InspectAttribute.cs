using System;

namespace InspectorReflector
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectAttribute : Attribute
    {
    }
}