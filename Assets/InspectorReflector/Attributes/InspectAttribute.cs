using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class InspectAttribute : Attribute
    {
    }
}