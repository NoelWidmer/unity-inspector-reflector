using System.Collections.Generic;

/// <summary>
///		Represents a group of records that can be collapsed in the inspector.
/// </summary>
public class InspectorFoldout : InspectorRecord
{
	/// <summary/>
	/// <param name="displayName">The name to use in the inspector.</param>
	public InspectorFoldout(string displayName) : base(InspectorRecordType.Foldout, displayName)
	{
	}

	/// <summary>
	///		Contains all records to display in this foldout.
	/// </summary>
	public readonly Dictionary<string, InspectorRecord> Records = new Dictionary<string, InspectorRecord>();

	/// <summary>
	///		True if the foldout is not collapsed.
	/// </summary>
	public bool IsOpen = true;
}