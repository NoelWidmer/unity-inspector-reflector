using System;
using System.Reflection;
using UnityEngine;

namespace InspectorReflector
{
    public class PropertyAndInspectAttribute
    {
        public PropertyAndInspectAttribute(PropertyInfo info, InspectAttribute inspectAttribute)
        {
            if(info == null)
                throw new ArgumentNullException("info");

            if(inspectAttribute == null)
                throw new ArgumentNullException("inspectAttribute");

            Info = info;
            InspectAttribute = inspectAttribute;
        }

        public PropertyInfo Info
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