using System;
using System.Reflection;

namespace InspectorReflector.Implementation
{
    public interface IMemberInspectionInfo
    {
        MemberInfo Info { get; }

        Type RealType { get; }

        InspectAttribute InspectAttribute { get; }

        bool CanRead { get; }

        bool CanWrite { get; }

        object GetValue(object target);
        void SetValue(object target, object value);
    }
}