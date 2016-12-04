using System;



public class InspectorAttribute : Attribute
{
	/// <summary>
	///		Marks a property for display in the inspector.
	/// </summary>
	public InspectorAttribute()
	{
	}


	/// <summary>
	///		Marks a property for display in the inspector.
	/// </summary>
	/// <param name="propertyPath">
	///		Denotes how a property should be grouped in the inspector. 
	///		If the <paramref name="propertyPath"/> ends with a "/" the actual name of the property will be used as the name of the property.
	///		Otherwise the last part of the path will be used as the name of the property.
	/// </param>
	public InspectorAttribute(string propertyPath)
	{
		if(propertyPath == null)
			throw new ArgumentNullException("propertyPath");

		string[] path = propertyPath.Split('/');

		if(path[path.Length - 1] != string.Empty)
		{
			PropertyName = path[path.Length - 1];
		}

		if(path.Length > 1)
		{
			FoldoutPath = new string[path.Length - 1];
			for(int i = 0; i < path.Length - 1; i++)
			{
				FoldoutPath[i] = path[i];
			}
		}
	}
	
	public readonly string PropertyName = null;
	public readonly string[] FoldoutPath = null;
}