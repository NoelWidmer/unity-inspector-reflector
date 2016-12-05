using System.Reflection;

public class InspectorProperty : InspectorRecord
{
	public InspectorProperty(PropertyInfo propertyInfo, string name, bool @readonly) : base(name)
	{
		Readonly = @readonly;
		PropertyInfo = propertyInfo;
	}

	public readonly bool Readonly;
	public readonly PropertyInfo PropertyInfo;
}