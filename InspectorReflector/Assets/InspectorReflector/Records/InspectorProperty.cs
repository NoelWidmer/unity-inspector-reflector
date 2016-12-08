using System.Reflection;

/// <summary>
///		Represents a C# property that can be shown in the inspector.
/// </summary>
public class InspectorProperty : InspectorMember
{
	/// <summary>
	///		Ignores the member name and uses the parameter <paramref name="displayName"/> instead.
	/// </summary>
	/// <param name="propertyInfo">The reflection info of the C# property.</param>
	/// <param name="displayName">The name to use in the inspector.</param>
	/// <param name="readonly">True if the record should be readonly. This only takes effect if <paramref name="propertyInfo"/> is not readonly.</param>
	public InspectorProperty(PropertyInfo propertyInfo, string displayName, bool @readonly) : base(
		propertyInfo,
		InspectorRecordType.Property,
		propertyInfo.PropertyType,
		displayName,
		IsReadonlyProperty(propertyInfo) ? true : @readonly)
	{
		PropertyInfo = propertyInfo;
	}

	/// <summary>
	///		Returnes true if a C# property cannot be read from.
	/// </summary>
	/// <param name="propertyInfo">The property to check.</param>
	private static bool IsReadonlyProperty(PropertyInfo propertyInfo)
	{
		return !propertyInfo.CanWrite || !propertyInfo.GetSetMethod(true).IsPublic;
	}

	/// <summary>
	///		The reflection info of the C# property.
	/// </summary>
	public readonly PropertyInfo PropertyInfo;
}