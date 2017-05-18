using System;
using System.Reflection;

namespace InspectorReflector
{
    public class PropertyAndInspectAttribute
    {
        public PropertyAndInspectAttribute(PropertyInfo propertyInfo, InspectAttribute inspectAttribute)
        {
            if(propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            if(inspectAttribute == null)
                throw new ArgumentNullException("inspectAttribute");

            PropertyInfo = propertyInfo;
            InspectAttribute = inspectAttribute;
        }

        public PropertyInfo PropertyInfo
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