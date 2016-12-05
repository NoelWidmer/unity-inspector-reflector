using System;
using System.Reflection;

public class InspectorData : InspectorFoldout
{
	public InspectorData() : base("Root")
	{
	}



	public InspectorProperty AddProperty(string[] foldouts, string propertyName, PropertyInfo propertyInfo, InspectorAttribute attribute)
	{
		if(foldouts == null)
			throw new ArgumentNullException("foldouts");

		InspectorFoldout foldoutToAddTo = this;

		if(foldouts.Length > 0)
		{
			for(int i = 0; i < foldouts.Length; i++)
			{
				InspectorFoldout newFoldout = new InspectorFoldout(foldouts[i]);
				InspectorRecord tmp;

				if(foldoutToAddTo.Records.TryGetValue(newFoldout.Name, out tmp))
				{
					if(tmp.IsFoldout)
					{
						foldoutToAddTo = (InspectorFoldout)tmp;
					}
					else
					{
						throw new InvalidOperationException("foldout path is not a foldout");
					}
				}
				else
				{
					foldoutToAddTo.Records.Add(newFoldout.Name, newFoldout);
					foldoutToAddTo = newFoldout;
				}
			}
		}

		InspectorProperty inspectorProperty = new InspectorProperty(propertyInfo, propertyName, attribute.Readonly);

		if(foldoutToAddTo.Records.ContainsKey(inspectorProperty.Name))
			throw new InvalidOperationException("name already exists in inspector.");

		foldoutToAddTo.Records.Add(inspectorProperty.Name, inspectorProperty);
		return inspectorProperty;
	}
}











