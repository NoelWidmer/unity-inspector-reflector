using System;
using System.Reflection;


public class InspectorData : InspectorFoldout
{
	public InspectorData() : base("Root") { }



	public InspectorRecord AddMember(string[] foldouts, MemberInfo memberInfo, InspectorAttribute attribute)
	{
		InspectorFoldout foldoutToAddTo = this;

		if(foldouts != null && foldouts.Length > 0)
		{
			for(int i = 0; i < foldouts.Length; i++)
			{
				InspectorFoldout newFoldout = new InspectorFoldout(foldouts[i]);
				InspectorRecord tmp;

				if(foldoutToAddTo.Records.TryGetValue(newFoldout.DisplayName, out tmp))
				{
					if(tmp.Type == InspectorRecordType.Foldout)
					{
						foldoutToAddTo = (InspectorFoldout)tmp;
					} else
					{
						throw new InvalidOperationException("foldout path is not a foldout");
					}
				} else
				{
					foldoutToAddTo.Records.Add(newFoldout.DisplayName, newFoldout);
					foldoutToAddTo = newFoldout;
				}
			}
		}

		InspectorMember inspectorMember;
		string recordName = attribute.PropertyName == null ? memberInfo.Name : attribute.PropertyName;

		if(memberInfo.MemberType == MemberTypes.Property)
		{
			inspectorMember = new InspectorProperty((PropertyInfo)memberInfo, recordName, attribute.Readonly);
		} else
		{
			inspectorMember = new InspectorField((FieldInfo)memberInfo, recordName, attribute.Readonly);
		}

		if(foldoutToAddTo.Records.ContainsKey(inspectorMember.DisplayName))
			throw new InvalidOperationException("name already exists in inspector.");

		foldoutToAddTo.Records.Add(inspectorMember.DisplayName, inspectorMember);
		return inspectorMember;
	}
}