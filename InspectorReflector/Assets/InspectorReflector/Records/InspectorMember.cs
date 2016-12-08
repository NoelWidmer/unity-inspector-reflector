using System;
using System.Reflection;

public abstract class InspectorMember : InspectorRecord
{
	/// <summary>
	///		Ignores the member name and uses the parameter <paramref name="displayName"/> instead.
	/// </summary>
	/// <param name="propertyInfo">The reflection info of the C# member.</param>
	/// <param name="displayName">The name to use in the inspector.</param>
	/// <param name="readonly">True if the record is readonly. For properties this only takes effect if <see cref="MemberInfo"/> is not readonly.</param>
	public InspectorMember(MemberInfo memberInfo, InspectorRecordType recordType, Type type, string displayName, bool @readonly) : base(recordType, displayName)
	{
		MemberInfo = memberInfo;
		Readonly = @readonly;
		ActualType = type;
	}

	/// <summary>
	///		True if the record is readonly. For properties this only takes effect if <see cref="MemberInfo"/> is not readonly.
	/// </summary>
	public readonly bool Readonly;

	/// <summary>
	///		The reflection info of the C# member.
	/// </summary>
	public readonly MemberInfo MemberInfo;

	/// <summary>
	///		The member's type.
	/// </summary>
	public readonly Type ActualType;
}