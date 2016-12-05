using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class InspectorReflector : Editor
{
	private static readonly string _defaultTag = "Untagged";

	private static int _instanceId = -1;
	private static InspectorData _data = null;

	private Dictionary<string, Func<string, object, object>> _defaultLookup = new Dictionary<string, Func<string, object, object>>();
	private Dictionary<string, Func<object, string>> _readonlyLookup = new Dictionary<string, Func<object, string>>();

	private Dictionary<string, Type> _intSliderAttributeLookup = new Dictionary<string, Type>();	
	private Dictionary<string, Func<int, object>> _sliderIntToX = new Dictionary<string, Func<int, object>>();
	private Dictionary<string, Func<object, int>> _sliderXToInt = new Dictionary<string, Func<object, int>>();


	public InspectorReflector()
	{
		//Readonly
		AddReadonly<string>((myself) => myself.ToString());
		AddReadonly<bool>((myself) => myself.ToString());
		AddReadonly<Bounds>((myself) => myself.ToString());
		AddReadonly<Vector2>((myself) => myself.ToString());
		AddReadonly<Vector3>((myself) => myself.ToString());
		AddReadonly<Vector4>((myself) => myself.ToString());
		AddReadonly<byte>((myself) => myself.ToString());
		AddReadonly<sbyte>((myself) => myself.ToString());
		AddReadonly<short>((myself) => myself.ToString());
		AddReadonly<ushort>((myself) => myself.ToString());
		AddReadonly<int>((myself) => myself.ToString());
		AddReadonly<uint>((myself) => myself.ToString());
		AddReadonly<long>((myself) => myself.ToString());
		AddReadonly<ulong>((myself) => myself.ToString());
		AddReadonly<float>((myself) => myself.ToString());
		AddReadonly<double>((myself) => myself.ToString());
		AddReadonly<decimal>((myself) => myself.ToString());

		//Readonly special
		AddReadonly<Rect>((myself) =>
		{
			Rect me = (Rect)myself;
			return string.Format("Origin: ({0}, {1}), Size: ({2}, {3})", me.x, me.y, me.width, me.height);
		});

		AddReadonly<Color>((myself) =>
		{
			Color me = (Color)myself;
			return string.Format("RGBA: ({0}, {1}, {2}, {3})", me.r, me.g, me.b, me.a);
		});

		//General
		AddCallback<string>((name, val) => EditorGUILayout.TextField(name, val.ToString()));
		AddCallback<bool>((name, val) => EditorGUILayout.Toggle(name, (bool)val));
		AddCallback<Bounds>((name, val) => EditorGUILayout.BoundsField(name, (Bounds)val));
		AddCallback<Rect>((name, val) => EditorGUILayout.RectField(name, (Rect)val));
		AddCallback<Color>((name, val) => EditorGUILayout.ColorField(name, (Color)val));
		AddCallback<AnimationCurve>((name, val) => EditorGUILayout.CurveField(name, (AnimationCurve)val));
		AddCallback<GameObject>((name, val) => EditorGUILayout.ObjectField(name, (GameObject)val, typeof(GameObject), false));
		AddCallback<Sprite>((name, val) => EditorGUILayout.ObjectField(name, (Sprite)val, typeof(Sprite), false));

		//Vectors
		AddCallback<Vector2>((name, val) => EditorGUILayout.Vector2Field(name, (Vector2)val));
		AddCallback<Vector3>((name, val) => EditorGUILayout.Vector3Field(name, (Vector3)val));
		AddCallback<Vector4>((name, val) => EditorGUILayout.Vector4Field(name, (Vector4)val));

		//Integral
		AddCallback<byte>((name, val) => (byte)EditorGUILayout.IntField(name, (byte)val));
		AddCallback<sbyte>((name, val) => (sbyte)EditorGUILayout.IntField(name, (sbyte)val));

		AddCallback<short>((name, val) => (short)EditorGUILayout.IntField(name, (short)val));
		AddCallback<ushort>((name, val) => (ushort)EditorGUILayout.IntField(name, (ushort)val));

		AddCallback<int>((name, val) => EditorGUILayout.IntField(name, (int)val));
		AddCallback<uint>((name, val) => (uint)EditorGUILayout.LongField(name, (uint)val));

		AddCallback<long>((name, val) => EditorGUILayout.LongField(name, (long)val));
		AddCallback<ulong>((name, val) => ulong.Parse(EditorGUILayout.TextField(name, val.ToString()))); //workaround

		//Floating points
		AddCallback<float>((name, val) => EditorGUILayout.FloatField(name, (float)val));
		AddCallback<double>((name, val) => EditorGUILayout.DoubleField(name, (double)val));
		AddCallback<decimal>((name, val) => decimal.Parse(EditorGUILayout.TextField(name, val.ToString()))); //workaround

		//Slideables
		AddSlideable<byte, InspectorSliderByteAttribute>((myself) => (byte)myself, (integer) => (byte)integer);
		AddSlideable<sbyte, InspectorSliderSByteAttribute>((myself) => (sbyte)myself, (integer) => (sbyte)integer);
		AddSlideable<short, InspectorSliderShortAttribute>((myself) => (short)myself, (integer) => (short)integer);
		AddSlideable<ushort, InspectorSliderUShortAttribute>((myself) => (ushort)myself, (integer) => (ushort)integer);
		AddSlideable<int, InspectorSliderIntAttribute>((myself) => (int)myself, (integer) => integer);
	}



	public void AddReadonly<T>(Func<object, string> call)
	{
		_readonlyLookup.Add(typeof(T).AssemblyQualifiedName, call);
	}



	public void AddCallback<T>(Func<string, object, object> call)
	{
		_defaultLookup.Add(typeof(T).AssemblyQualifiedName, call);
	}



	public void AddSlideable<TType, TAttribute>(Func<object, int> xToInt, Func<int, object> intToX) where TAttribute : InspectorSliderIntAttribute
	{
		_sliderXToInt.Add(typeof(TType).AssemblyQualifiedName, xToInt);
		_sliderIntToX.Add(typeof(TType).AssemblyQualifiedName, intToX);
		_intSliderAttributeLookup.Add(typeof(TType).AssemblyQualifiedName, typeof(TAttribute));
	}



	public override void OnInspectorGUI()
	{
		if(_instanceId != target.GetHashCode())
		{
			_instanceId = target.GetHashCode();
			_data = new InspectorData();

			var properties = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach(var property in properties)
			{
				if(property.GetIndexParameters().Length != 0)
					continue;

				if(!Attribute.IsDefined(property, typeof(InspectorAttribute)))
					continue;

				if(property.CanRead)
				{
					var attribute = (InspectorAttribute)Attribute.GetCustomAttribute(property, typeof(InspectorAttribute));
					string[] foldouts = attribute.FoldoutPath == null ? new string[] { } : attribute.FoldoutPath;
					_data.AddProperty(foldouts, attribute.PropertyName == null ? property.Name : attribute.PropertyName, property, attribute);
				}
			}
		}

		DrawFoldout(_data, target);
	}



	private void DrawFoldout(InspectorFoldout foldout, object instance)
	{
		foreach(var keyValue in foldout.Records)
		{
			InspectorRecord inspectorRecord = keyValue.Value;

			if(inspectorRecord.IsFoldout)
			{
				InspectorFoldout inspectorFoldout = (InspectorFoldout)inspectorRecord;
				inspectorFoldout.IsOpen = EditorGUILayout.Foldout(inspectorFoldout.IsOpen, inspectorFoldout.Name);

				if(inspectorFoldout.IsOpen)
				{
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.BeginVertical();
					EditorGUI.indentLevel++;

					DrawFoldout(inspectorFoldout, instance);

					EditorGUI.indentLevel--;
					EditorGUILayout.EndVertical();
					EditorGUILayout.EndHorizontal();
				}
			}
			else
			{
				InspectorProperty inspectorProperty = (InspectorProperty)inspectorRecord;
				UpdateProperty(inspectorProperty, instance);
			}
		}
	}



	private void UpdateProperty(InspectorProperty inspectorProperty, object instance)
	{
		string aqtn = inspectorProperty.PropertyInfo.PropertyType.AssemblyQualifiedName;
		object origVal = inspectorProperty.PropertyInfo.GetValue(instance, null);

		if(!inspectorProperty.PropertyInfo.CanWrite || !inspectorProperty.PropertyInfo.GetSetMethod(true).IsPublic || inspectorProperty.Readonly)
		{
			Func<object, string> call;

			if(_readonlyLookup.TryGetValue(aqtn, out call))
			{
				DisplayReadonlyProperty(inspectorProperty, call(origVal));
			}
			else
			{
				if(inspectorProperty.PropertyInfo.PropertyType.IsEnum)
				{
					DisplayReadonlyProperty(inspectorProperty, origVal);
				} else {
					throw new NotSupportedException("The following type is not yet supported for readonly viewing: " + aqtn);
				}
			}
		}
		else
		{
			Type popertyType = inspectorProperty.PropertyInfo.PropertyType;
			object newVal;

			if(popertyType.IsEnum)
			{
				newVal = UpdateEnumProperty(inspectorProperty, origVal);
			}
			else if(popertyType == typeof(string) && Attribute.IsDefined(inspectorProperty.PropertyInfo, typeof(InspectorTagAttribute)))
			{
				newVal = UpdateTagProperty(inspectorProperty, origVal);
			}
			else if(popertyType == typeof(Vector2) && Attribute.IsDefined(inspectorProperty.PropertyInfo, typeof(InspectorSliderVector2Attribute)))
			{
				newVal = UpdateVector2SliderProperty(inspectorProperty, origVal);
			}
			else if(_intSliderAttributeLookup.ContainsKey(aqtn) && Attribute.IsDefined(inspectorProperty.PropertyInfo, typeof(InspectorSliderIntAttribute)))
			{
				newVal = UpdateIntSliderProperty(inspectorProperty, origVal);
			}
			else if(popertyType == typeof(float) && Attribute.IsDefined(inspectorProperty.PropertyInfo, typeof(InspectorSliderFloatAttribute)))
			{
				newVal = UpdateFloatSliderProperty(inspectorProperty, origVal);
			}
			else
			{
				newVal = UpdateStandardProperty(inspectorProperty, origVal);
			}

			if(newVal != origVal)
			{
				inspectorProperty.PropertyInfo.SetValue(instance, newVal, null);
				EditorUtility.SetDirty((UnityEngine.Object)instance);
			}
		}
	}



	private void DisplayReadonlyProperty(InspectorProperty inspectorProperty, object origVal)
	{
		EditorGUILayout.LabelField(inspectorProperty.Name, origVal.ToString());
	}



	private object UpdateEnumProperty(InspectorProperty inspectorProperty, object origVal)
	{
		return EditorGUILayout.EnumPopup(inspectorProperty.Name, (Enum)origVal);
	}



	private object UpdateTagProperty(InspectorProperty inspectorProperty, object origVal)
	{
		object newVal = EditorGUILayout.TagField(inspectorProperty.Name, origVal == null ? null : origVal.ToString());

		if(newVal == null || ((string)newVal) == string.Empty)
		{
			return _defaultTag;
		}

		return newVal;
	}



	private object UpdateVector2SliderProperty(InspectorProperty inspectorProperty, object origVal)
	{
		Vector2 origVector2 = (Vector2)origVal;
		InspectorSliderVector2Attribute attr = (InspectorSliderVector2Attribute)Attribute.GetCustomAttribute(inspectorProperty.PropertyInfo, typeof(InspectorSliderVector2Attribute));
		EditorGUILayout.MinMaxSlider(new GUIContent(inspectorProperty.Name), ref origVector2.x, ref origVector2.y, attr.MinLimit, attr.MaxLimit, null);

		if(attr.ShowValues)
		{
			EditorGUILayout.LabelField(null as string, string.Format("{0} - {1}", origVector2.x, origVector2.y));
		}

		return origVector2;
	}



	private object UpdateIntSliderProperty(InspectorProperty inspectorProperty, object origVal)
	{
		string aqtn = inspectorProperty.PropertyInfo.PropertyType.AssemblyQualifiedName;

		var intToX = _sliderIntToX[aqtn];
		var xToInt = _sliderXToInt[aqtn];
		var attrType = _intSliderAttributeLookup[aqtn];

		if(!Attribute.IsDefined(inspectorProperty.PropertyInfo, attrType))
		{
			throw new NotSupportedException("expected an another attribute.");
		}

		InspectorSliderIntAttribute attr = (InspectorSliderIntAttribute)Attribute.GetCustomAttribute(inspectorProperty.PropertyInfo, attrType);
		int result = EditorGUILayout.IntSlider(new GUIContent(inspectorProperty.Name), xToInt(origVal), attr.Min, attr.Max, null);
		return intToX(result);
	}



	private object UpdateFloatSliderProperty(InspectorProperty inspectorProperty, object origVal)
	{
		InspectorSliderFloatAttribute attr = (InspectorSliderFloatAttribute)Attribute.GetCustomAttribute(inspectorProperty.PropertyInfo, typeof(InspectorSliderFloatAttribute));
		return EditorGUILayout.Slider(new GUIContent(inspectorProperty.Name), (float)origVal, attr.Min, attr.Max, null);
	}



	private object UpdateStandardProperty(InspectorProperty inspectorProperty, object origVal)
	{
		Func<string, object, object> createInspectorRecord;
		string aqtn = inspectorProperty.PropertyInfo.PropertyType.AssemblyQualifiedName;

		if(_defaultLookup.TryGetValue(aqtn, out createInspectorRecord))
		{
			return createInspectorRecord(inspectorProperty.Name, origVal);
		}
		else
		{
			throw new NotSupportedException("The following type is not yet supported: " + aqtn);
		}
	}
}