using System.Reflection;

namespace InspectorReflector
{
    public class PropertyAndInspectAttribute
    {
        public PropertyAndInspectAttribute(PropertyInfo property, InspectAttribute inspectAttribute)
        {
            Property = property;
            InspectAttribute = inspectAttribute;
        }

        public PropertyInfo Property
        {
            get;
            private set;
        }
        public InspectAttribute InspectAttribute
        {
            get;
            private set;
        }
    }
}