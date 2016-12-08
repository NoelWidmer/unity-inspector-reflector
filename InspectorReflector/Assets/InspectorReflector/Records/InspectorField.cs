using System.Reflection;

/// <summary>
///		Represents a C# field that can be shown in the inspector.
/// </summary>
public class InspectorField : InspectorMember
{
	/// <summary>
	///		Ignores the member name and uses the parameter <paramref name="displayName"/> instead.
	/// </summary>
	/// <param name="fieldInfo">The reflection info of the C# field.</param>
	/// <param name="displayName">The name to use in the inspector.</param>
	/// <param name="readonly">True if the record is readonly.</param>
	public InspectorField(FieldInfo fieldInfo, string displayName, bool @readonly) : base(
		fieldInfo,
		InspectorRecordType.Field,
		fieldInfo.FieldType,
		displayName,
		@readonly)
	{
		FieldInfo = fieldInfo;
	}

	/// <summary>
	///		The reflection info of the C# field.
	/// </summary>
	public readonly FieldInfo FieldInfo;
}