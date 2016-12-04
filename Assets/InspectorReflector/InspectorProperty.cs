using System.Reflection;

public class InspectorProperty : InspectorRecord
{
	public InspectorProperty(PropertyInfo propertyInfo, string name) : base(name)
	{
		PropertyInfo = propertyInfo;
	}

	public readonly PropertyInfo PropertyInfo;
}