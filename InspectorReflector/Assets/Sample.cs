using UnityEngine;

public class Sample : MonoBehaviour
{
	#region Readonly

	[Inspector("Readonly/String")]
	public string ReadonlyString {
		get {
			return _aString;
		}
	}

	[Inspector("Readonly/Boolean")]
	public bool ReadonlyBool {
		get {
			return _aBool;
		}
	}

	[Inspector("Readonly/Bounds")]
	public Bounds ReadonlyBounds {
		get {
			return _aBounds;
		}
	}

	[Inspector("Readonly/Rect")]
	public Rect ReadonlyRect {
		get {
			return _aRect;
		}
	}

	[Inspector("Readonly/Color")]
	public Color ReadonlyColor {
		get {
			return _aColor;
		}
	}

	//[Inspector("Readonly/Curve")]
	public AnimationCurve ReadonlyCurve {
		get {
			return _aCurve;
		}
	}

	[Inspector("Readonly/Vector2")]
	public Vector2 ReadonlyVector2 {
		get {
			return _aVector2;
		}
	}

	[Inspector("Readonly/Vector3")]
	public Vector3 ReadonlyVector3 {
		get {
			return _aVector3;
		}
	}

	[Inspector("Readonly/Vector4")]
	public Vector4 ReadonlyVector4 {
		get {
			return _aVector4;
		}
	}

	[Inspector("Readonly/Byte")]
	public byte ReadonlyByte {
		get {
			return _aByte;
		}
	}

	[Inspector("Readonly/SByte")]
	public sbyte ReadonlySByte {
		get {
			return _anSByte;
		}
	}

	[Inspector("Readonly/Short")]
	public short ReadonlyShort {
		get {
			return _aShort;
		}
	}

	[Inspector("Readonly/UShort")]
	public ushort ReadonlyUShort {
		get {
			return _anUShort;
		}
	}

	[Inspector("Readonly/Int")]
	public int ReadonlyInt {
		get {
			return _anInt;
		}
	}

	[Inspector("Readonly/UInt")]
	public uint ReadonlyUInt {
		get {
			return _anUInt;
		}
	}

	[Inspector("Readonly/Long")]
	public long ReadonlyLong {
		get {
			return _aLong;
		}
	}

	[Inspector("Readonly/ULong")]
	public ulong ReadonlyULong {
		get {
			return _aULong;
		}
	}

	[Inspector("Readonly/Enum")]
	public StackTraceLogType ReadonlyEnum {
		get {
			return _anEnum;
		}
	}

	[Inspector("Readonly/Float")]
	public float ReadonlyFloat {
		get {
			return _aFloat;
		}
	}

	[Inspector("Readonly/Double")]
	public double ReadonlyDouble {
		get {
			return _aDouble;
		}
	}

	[Inspector("Readonly/Decimal")]
	public decimal ReadonlyDecimal {
		get {
			return _aDecimal;
		}
	}

	#endregion

	#region General

	#region String

	[SerializeField]
	private string _aString = "Write here: ";
	[Inspector("General/String/")]
	public string WriteableString {
		get {
			return _aString;
		}
		set {
			_aString = value;
		}
	}

	[SerializeField]
	private string _aTag;
	[InspectorTag("General/String/")]
	public string ATag {
		get {
			return _aTag;
		}
		set {
			_aTag = value;
		}
	}

	#endregion

	[SerializeField]
	private bool _aBool = true;
	[Inspector("General/ABoolean")]
	public bool ABool {
		get {
			return _aBool;
		}
		set {
			_aBool = value;
		}
	}

	[SerializeField]
	private Bounds _aBounds;
	[Inspector("General/")]
	public Bounds ABounds {
		get {
			return _aBounds;
		}
		set {
			_aBounds = value;
		}
	}

	[SerializeField]
	private Rect _aRect;
	[Inspector("General/")]
	public Rect ARect {
		get {
			return _aRect;
		}
		set {
			_aRect = value;
		}
	}

	[SerializeField]
	private Color _aColor;
	[Inspector("General/")]
	public Color AColor {
		get {
			return _aColor;
		}
		set {
			_aColor = value;
		}
	}

	[SerializeField]
	private AnimationCurve _aCurve;
	[Inspector("General/")]
	public AnimationCurve ACurve {
		get {
			return _aCurve;
		}
		set {
			_aCurve = value;
		}
	}

	#endregion

	#region Vectors

	[SerializeField]
	private Vector2 _aVector2;
	[Inspector("Vectors/")]
	public Vector2 AVector2 {
		get {
			return _aVector2;
		}
		set {
			_aVector2 = value;
		}
	}

	[SerializeField]
	private Vector3 _aVector3;
	[Inspector("Vectors/")]
	public Vector3 AVector3 {
		get {
			return _aVector3;
		}
		set {
			_aVector3 = value;
		}
	}

	[SerializeField]
	private Vector4 _aVector4;
	[Inspector("Vectors/")]
	public Vector4 AVector4 {
		get {
			return _aVector4;
		}
		set {
			_aVector4 = value;
		}
	}

	#region Sliders

	[SerializeField]
	private Vector2 _aVector2Slider;
	[InspectorSliderVector2("Vectors/Sliders/", 0f, 10f, true)]
	public Vector2 AVector2Slider {
		get {
			return _aVector2Slider;
		}
		set {
			_aVector2Slider = value;
		}
	}

	#endregion

	#endregion

	#region Integral

	[SerializeField]
	private byte _aByte = 5;
	[Inspector("Integral/")]
	public byte AByte {
		get {
			return _aByte;
		}
		set {
			_aByte = value;
		}
	}

	[SerializeField]
	private sbyte _anSByte = 5;
	[Inspector("Integral/")]
	public sbyte AnSByte {
		get {
			return _anSByte;
		}
		set {
			_anSByte = value;
		}
	}

	[SerializeField]
	private short _aShort = 5;
	[Inspector("Integral/")]
	public short AShort {
		get {
			return _aShort;
		}
		set {
			_aShort = value;
		}
	}

	[SerializeField]
	private ushort _anUShort = 5;
	[Inspector("Integral/")]
	public ushort AnUShort {
		get {
			return _anUShort;
		}
		set {
			_anUShort = value;
		}
	}

	[SerializeField]
	private int _anInt = 5;
	[Inspector("Integral/")]
	public int AnInt {
		get {
			return _anInt;
		}
		set {
			_anInt = value;
		}
	}

	[SerializeField]
	private uint _anUInt = 5;
	[Inspector("Integral/")]
	public uint AnUInt {
		get {
			return _anUInt;
		}
		set {
			_anUInt = value;
		}
	}

	[SerializeField]
	private long _aLong = 5;
	[Inspector("Integral/")]
	public long ALong {
		get {
			return _aLong;
		}
		set {
			_aLong = value;
		}
	}

	[SerializeField]
	private ulong _aULong = 5;
	[Inspector("Integral/")]
	public ulong AULong {
		get {
			return _aULong;
		}
		set {
			_aULong = value;
		}
	}

	#region Sliders

	[SerializeField]
	private byte _aByteSlider = 5;
	[InspectorSliderByte("Integral/Sliders/", byte.MinValue, byte.MaxValue)]
	public byte AByteSlider {
		get {
			return _aByteSlider;
		}
		set {
			_aByteSlider = value;
		}
	}

	[SerializeField]
	private sbyte _anSByteSlider = 5;
	[InspectorSliderSByte("Integral/Sliders/", sbyte.MinValue, sbyte.MaxValue)]
	public sbyte AnSByteSlider {
		get {
			return _anSByteSlider;
		}
		set {
			_anSByteSlider = value;
		}
	}

	[SerializeField]
	private short _aShortSlider = 5;
	[InspectorSliderShort("Integral/Sliders/", short.MinValue, short.MaxValue)]
	public short AShortSlider {
		get {
			return _aShortSlider;
		}
		set {
			_aShortSlider = value;
		}
	}

	[SerializeField]
	private ushort _anUShortSlider = 5;
	[InspectorSliderUShort("Integral/Sliders/", ushort.MinValue, ushort.MaxValue)]
	public ushort AnUShortSlider {
		get {
			return _anUShortSlider;
		}
		set {
			_anUShortSlider = value;
		}
	}

	[SerializeField]
	private int _anIntSlider = 5;
	[InspectorSliderInt("Integral/Sliders/", int.MinValue, int.MaxValue)]
	public int AnIntSlider {
		get {
			return _anIntSlider;
		}
		set {
			_anIntSlider = value;
		}
	}

	#endregion

	#region Special

	[SerializeField]
	private StackTraceLogType _anEnum;
	[Inspector("Integral/Special/")]
	public StackTraceLogType AnEnum {
		get {
			return _anEnum;
		}
		set {
			_anEnum = value;
		}
	}

	#endregion

	#endregion

	#region Floating points
	[SerializeField]
	private float _aFloat = 5;
	[Inspector("Floating points/")]
	public float AFloat {
		get {
			return _aFloat;
		}
		set {
			_aFloat = value;
		}
	}

	[SerializeField]
	private double _aDouble = 5;
	[Inspector("Floating points/")]
	public double ADouble {
		get {
			return _aDouble;
		}
		set {
			_aDouble = value;
		}
	}

	[SerializeField]
	private decimal _aDecimal = 5;
	[Inspector("Floating points/")]
	public decimal ADecimal {
		get {
			return _aDecimal;
		}
		set {
			_aDecimal = value;
		}
	}

	#region Sliders

	[SerializeField]
	private float _aFloatSlider = 5;
	[InspectorSliderFloat("Floating points/Sliders/", float.MinValue, float.MaxValue)]
	public float AFloatSlider {
		get {
			return _aFloatSlider;
		}
		set {
			_aFloatSlider = value;
		}
	}

	#endregion

	#endregion
}