using System;
using System.Reflection;

namespace InspectorReflector.Implementation
{
    public class FieldInspectionInfo : IMemberInspectionInfo
    {
        public FieldInspectionInfo(FieldInfo info, InspectAttribute inspectAttribute)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            RealType = info.FieldType ?? throw new ArgumentException("Is null.", nameof(info.FieldType));
            InspectAttribute = inspectAttribute ?? throw new ArgumentNullException(nameof(inspectAttribute));
        }

        public FieldInfo Info { get; }
        MemberInfo IMemberInspectionInfo.Info => Info;

        public Type RealType { get; }

        public InspectAttribute InspectAttribute { get; }

        public bool CanRead => true;

        public bool CanWrite => Info.IsInitOnly == false && Info.IsLiteral == false;

        public object GetValue(object target) => Info.GetValue(target);

        public void SetValue(object target, object newValue) => Info.SetValue(target, newValue);
    }
}