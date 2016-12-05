public class InspectorTagAttribute : InspectorAttribute
{
	public InspectorTagAttribute(bool @readonly = false) : base(@readonly)
	{
	}

	public InspectorTagAttribute(string path, bool @readonly = false) : base(path, @readonly)
	{
	}
}