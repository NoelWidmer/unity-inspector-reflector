using System;
using System.Reflection;

namespace InspectorReflector.Implementation
{
    public class PropertyInspectionInfo : IMemberInspectionInfo
    {
        public PropertyInspectionInfo(PropertyInfo info, InspectAttribute inspectAttribute)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            RealType = info.PropertyType ?? throw new ArgumentException("Is null.", nameof(info.PropertyType));
            InspectAttribute = inspectAttribute ?? throw new ArgumentNullException(nameof(inspectAttribute));
        }

        public PropertyInfo Info { get; }
        MemberInfo IMemberInspectionInfo.Info => Info;

        public Type RealType { get; }

        public InspectAttribute InspectAttribute { get; }

        public bool CanRead => Info.CanRead;

        public bool CanWrite => Info.CanWrite;

        public object GetValue(object target) => Info.GetValue(target, null);

        public void SetValue(object target, object newValue) => Info.SetValue(target, newValue, null);
    }
}