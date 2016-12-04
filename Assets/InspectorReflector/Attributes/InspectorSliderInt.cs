using System;

public class InspectorSliderIntAttribute : InspectorAttribute
{
	public InspectorSliderIntAttribute(int min, int max) : base()
	{
		if(max < min)
			throw new ArgumentException("max cannot be smaller than min.");

		Min = min;
		Max = max;
	}

	public InspectorSliderIntAttribute(string path, int min, int max) : base(path)
	{
		if(max < min)
			throw new ArgumentException("max cannot be smaller than min.");

		Min =  min;
		Max =  max;
	}

	public readonly int Min;
	public readonly int Max;
}



public class InspectorSliderByteAttribute : InspectorSliderIntAttribute
{
	public InspectorSliderByteAttribute(byte min, byte max) : base(min, max)
	{
	}

	public InspectorSliderByteAttribute(string path, byte min, byte max) : base(path, min, max)
	{
	}
}



public class InspectorSliderSByteAttribute : InspectorSliderIntAttribute
{
	public InspectorSliderSByteAttribute(sbyte min, sbyte max) : base(min, max)
	{
	}

	public InspectorSliderSByteAttribute(string path, sbyte min, sbyte max) : base(path, min, max)
	{
	}
}



public class InspectorSliderShortAttribute : InspectorSliderIntAttribute
{
	public InspectorSliderShortAttribute(short min, short max) : base(min, max)
	{
	}

	public InspectorSliderShortAttribute(string path, short min, short max) : base(path, min, max)
	{
	}
}



public class InspectorSliderUShortAttribute : InspectorSliderIntAttribute
{
	public InspectorSliderUShortAttribute(ushort min, ushort max) : base(min, max)
	{
	}

	public InspectorSliderUShortAttribute(string path, ushort min, ushort max) : base(path, min, max)
	{
	}
}



public class InspectorSliderFloatAttribute : InspectorAttribute
{
	public InspectorSliderFloatAttribute(float min, float max) : base()
	{
		if(max < min)
			throw new ArgumentException("max cannot be smaller than min.");

		Min = min;
		Max = max;
	}

	public InspectorSliderFloatAttribute(string path, float min, float max) : base(path)
	{
		if(max < min)
			throw new ArgumentException("max cannot be smaller than min.");

		Min = min;
		Max = max;
	}

	public readonly float Min;
	public readonly float Max;
}