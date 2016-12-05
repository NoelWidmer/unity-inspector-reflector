public class InspectorSliderVector2Attribute : InspectorAttribute
{
	public InspectorSliderVector2Attribute(float minLimit, float maxLimit, bool showValues = false) : base()
	{
		MinLimit = minLimit;
		MaxLimit = maxLimit;
		ShowValues = showValues;
	}

	public InspectorSliderVector2Attribute(string path, float minLimit, float maxLimit, bool showValues = false) : base(path)
	{
		MinLimit = minLimit;
		MaxLimit = maxLimit;
		ShowValues = showValues;
	}

	public readonly float MinLimit;
	public readonly float MaxLimit;
	public readonly bool ShowValues;
}