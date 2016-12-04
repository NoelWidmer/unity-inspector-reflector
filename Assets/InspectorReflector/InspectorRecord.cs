using UnityEngine;
using System.Collections;

public class InspectorRecord
{
	public InspectorRecord(string name)
	{
		Name = name;
	}

	public readonly string Name;

	public bool IsFoldout
	{
		get
		{
			return this is InspectorFoldout;
		}
	}
}