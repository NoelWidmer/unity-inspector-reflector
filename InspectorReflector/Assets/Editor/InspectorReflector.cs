using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


public class InspectorReflector : Editor
{
	private static readonly string _defaultTag = "Untagged";

	private static int _targetId = -1;
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
		AddReadonly<Rect>((myself) => {
			Rect me = (Rect)myself;
			return string.Format("Origin: ({0}, {1}), Size: ({2}, {3})", me.x, me.y, me.width, me.height);
		});

		AddReadonly<Color>((myself) => {
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



	private void AddReadonly<T>(Func<object, string> call)
	{
		_readonlyLookup.Add(typeof(T).AssemblyQualifiedName, call);
	}



	private void AddCallback<T>(Func<string, object, object> call)
	{
		_defaultLookup.Add(typeof(T).AssemblyQualifiedName, call);
	}



	private void AddSlideable<TType, TAttribute>(Func<object, int> xToInt, Func<int, object> intToX) where TAttribute : InspectorSliderIntAttribute
	{
		_sliderXToInt.Add(typeof(TType).AssemblyQualifiedName, xToInt);
		_sliderIntToX.Add(typeof(TType).AssemblyQualifiedName, intToX);
		_intSliderAttributeLookup.Add(typeof(TType).AssemblyQualifiedName, typeof(TAttribute));
	}



	public override void OnInspectorGUI()
	{
		try
		{
			Undo.RecordObject(target, "test");
			Draw(target);
		} catch(Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}



	private void Draw(object victim)
	{
		if(_targetId != victim.GetHashCode())
		{
			_targetId = victim.GetHashCode();
			_data = new InspectorData();

			var members = victim.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);

			if(members != null)
			{
				foreach(var member in members)
				{
					if(member.MemberType == MemberTypes.Property || member.MemberType == MemberTypes.Field)
					{
						if(!Attribute.IsDefined(member, typeof(InspectorAttribute)))
							continue;

						if(member.MemberType == MemberTypes.Property)
						{
							PropertyInfo property = (PropertyInfo)member;

							if(!property.CanRead)
								continue; //TODO maybe show an inspector warning?

							var indexParams = property.GetIndexParameters();
							if(indexParams == null || indexParams.Length != 0)
								continue; //TODO maybe show an inspector warning?
						}

						var attribute = (InspectorAttribute)Attribute.GetCustomAttribute(member, typeof(InspectorAttribute));

						_data.AddMember(attribute.FoldoutPath, member, attribute);
					}
				}
			}
		}

		DrawFoldout(_data, victim);
	}



	private void DrawFoldout(InspectorFoldout foldout, object victim)
	{
		if(foldout.Records != null)
		{
			foreach(var keyValue in foldout.Records)
			{
				InspectorRecord inspectorRecord = keyValue.Value;

				if(inspectorRecord.Type == InspectorRecordType.Foldout)
				{
					InspectorFoldout inspectorFoldout = (InspectorFoldout)inspectorRecord;
					inspectorFoldout.IsOpen = EditorGUILayout.Foldout(inspectorFoldout.IsOpen, inspectorFoldout.DisplayName);

					if(inspectorFoldout.IsOpen)
					{
						EditorGUI.indentLevel++;

						//try
						//{
							DrawFoldout(inspectorFoldout, victim);
						/*} catch(Exception ex)
						{
							Debug.LogError(string.Format("InspectorReflector: {0}", ex.Message));
						}*/

						EditorGUI.indentLevel--;
					}
				} else if(inspectorRecord.Type == InspectorRecordType.Property)
				{
					InspectorProperty inspectorProperty = (InspectorProperty)inspectorRecord;
					DrawMember(
						inspectorProperty,
						victim,
						(__obj) => inspectorProperty.PropertyInfo.GetValue(__obj, null),
						(__obj, __newVal) => inspectorProperty.PropertyInfo.SetValue(__obj, __newVal, null)
						);
				} else
				{
					InspectorField inspectorField = (InspectorField)inspectorRecord;
					DrawMember(
						inspectorField,
						victim,
						(__obj) => inspectorField.FieldInfo.GetValue(__obj),
						(__obj, __newVal) => inspectorField.FieldInfo.SetValue(__obj, __newVal)
						);
				}
			}
		}
	}



	private void DrawMember(InspectorMember inspectorMember, object victim, Func<object, object> get, Action<object, object> set)
	{
		string aqtn = inspectorMember.ActualType.AssemblyQualifiedName;
		object origVal = get(victim);

		if(inspectorMember.Readonly)
		{
			Func<object, string> call;

			if(_readonlyLookup.TryGetValue(aqtn, out call))
			{
				DisplayReadonlyMember(inspectorMember, call(origVal));
			} else
			{
				if(inspectorMember.ActualType.IsEnum)
				{
					DisplayReadonlyMember(inspectorMember, origVal);
				} else
				{
					throw new NotSupportedException("The following type is not yet supported for readonly viewing: " + aqtn);
				}
			}
		} 
		else
		{
			object newVal;

			if(inspectorMember.ActualType.IsEnum)
			{
				newVal = UpdateEnumMember(inspectorMember, origVal);
			} else if(inspectorMember.ActualType == typeof(string) && Attribute.IsDefined(inspectorMember.MemberInfo, typeof(InspectorTagAttribute)))
			{
				newVal = UpdateTagMember(inspectorMember, origVal);
			} else if(inspectorMember.ActualType == typeof(Vector2) && Attribute.IsDefined(inspectorMember.MemberInfo, typeof(InspectorSliderVector2Attribute)))
			{
				newVal = UpdateVector2SliderMember(inspectorMember, origVal);
			} else if(_intSliderAttributeLookup.ContainsKey(aqtn) && Attribute.IsDefined(inspectorMember.MemberInfo, typeof(InspectorSliderIntAttribute)))
			{
				newVal = UpdateIntSliderMember(inspectorMember, origVal);
			} else if(inspectorMember.ActualType == typeof(float) && Attribute.IsDefined(inspectorMember.MemberInfo, typeof(InspectorSliderFloatAttribute)))
			{
				newVal = UpdateFloatSliderMember(inspectorMember, origVal);
			} else
			{
				newVal = ÜpdateStandardMember(inspectorMember, origVal);
			}

			if(newVal != origVal)
			{
				set(victim, newVal);
				//EditorUtility.SetDirty((UnityEngine.Object)victim);
			}
		}
	}

	#region Display member

	private void DisplayReadonlyMember(InspectorMember inspectorMember, object origVal)
	{
		EditorGUILayout.LabelField(inspectorMember.DisplayName, origVal.ToString());
	}



	private object UpdateEnumMember(InspectorMember inspectorMember, object origVal)
	{
		return EditorGUILayout.EnumPopup(inspectorMember.DisplayName, (Enum)origVal);
	}



	private object UpdateTagMember(InspectorMember inspectorMember, object origVal)
	{
		object newVal = EditorGUILayout.TagField(inspectorMember.DisplayName, origVal == null ? null : origVal.ToString());

		if(newVal == null || ((string)newVal) == string.Empty)
		{
			return _defaultTag;
		}

		return newVal;
	}



	private object UpdateVector2SliderMember(InspectorMember inspectorMember, object origVal)
	{
		Vector2 origVector2 = (Vector2)origVal;
		InspectorSliderVector2Attribute attr = (InspectorSliderVector2Attribute)Attribute.GetCustomAttribute(inspectorMember.MemberInfo, typeof(InspectorSliderVector2Attribute));
		EditorGUILayout.MinMaxSlider(new GUIContent(inspectorMember.DisplayName), ref origVector2.x, ref origVector2.y, attr.MinLimit, attr.MaxLimit, null);

		if(attr.ShowValues)
		{
			EditorGUILayout.LabelField(null as string, string.Format("{0} - {1}", origVector2.x, origVector2.y));
		}

		return origVector2;
	}



	private object UpdateIntSliderMember(InspectorMember inspectorMember, object origVal)
	{
		string aqtn = inspectorMember.ActualType.AssemblyQualifiedName;

		var intToX = _sliderIntToX[aqtn];
		var xToInt = _sliderXToInt[aqtn];
		var attrType = _intSliderAttributeLookup[aqtn];

		if(!Attribute.IsDefined(inspectorMember.MemberInfo, attrType))
			throw new NotSupportedException("expected an another attribute.");

		InspectorSliderIntAttribute attr = (InspectorSliderIntAttribute)Attribute.GetCustomAttribute(inspectorMember.MemberInfo, attrType);
		int result = EditorGUILayout.IntSlider(new GUIContent(inspectorMember.DisplayName), xToInt(origVal), attr.Min, attr.Max, null);
		return intToX(result);
	}



	private object UpdateFloatSliderMember(InspectorMember inspectorMember, object origVal)
	{
		InspectorSliderFloatAttribute attr = (InspectorSliderFloatAttribute)Attribute.GetCustomAttribute(inspectorMember.MemberInfo, typeof(InspectorSliderFloatAttribute));
		return EditorGUILayout.Slider(new GUIContent(inspectorMember.DisplayName), (float)origVal, attr.Min, attr.Max, null);
	}



	private object ÜpdateStandardMember(InspectorMember inspectorMember, object origVal)
	{
		string aqtn = inspectorMember.ActualType.AssemblyQualifiedName;

		Func<string, object, object> drawDefaultInspectorMember;

		if(_defaultLookup.TryGetValue(aqtn, out drawDefaultInspectorMember))
		{
			return drawDefaultInspectorMember(inspectorMember.DisplayName, origVal);
		} else
		{
			throw new NotSupportedException("The following type is not yet supported: " + aqtn);
		}
	}

	#endregion
}