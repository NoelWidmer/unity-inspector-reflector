using System.Collections.Generic;

public class InspectorFoldout : InspectorRecord
{
	public InspectorFoldout(string name) : base(name)
	{
	}

	public readonly Dictionary<string, InspectorRecord> Records = new Dictionary<string, InspectorRecord>();
	public bool IsOpen = true;
}