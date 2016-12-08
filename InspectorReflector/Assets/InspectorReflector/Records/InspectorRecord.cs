using System;

/// <summary>
///		Either an <see cref="InspectorFoldout"/>, an <see cref="InspectorProperty"/> or an <see cref="InspectorField"/> that can be shown in the inspector.
/// </summary>
public abstract class InspectorRecord
{
	/// <summary>
	///		Ignores the member name and uses the parameter <paramref name="displayName"/> instead.
	/// </summary>
	/// <param name="type">The record type of this instance.</param>
	/// <param name="displayName">The name to use in the inspector.</param>
	public InspectorRecord(InspectorRecordType type, string displayName)
	{
		if(displayName == null)
			throw new ArgumentNullException("name");

		if(displayName == string.Empty)
			throw new ArgumentException("name cannot be empty.");

		if(displayName.Trim().Length == 0)
			throw new ArgumentException("name cannot be whitespace only.");

		Type = type;
		DisplayName = displayName;
	}

	/// <summary>
	///		The record type of this instance.
	/// </summary>
	public readonly InspectorRecordType Type;

	/// <summary>
	///		The name to use in the inspector.
	/// </summary>
	public readonly string DisplayName;
}