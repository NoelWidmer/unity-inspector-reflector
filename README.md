# Unity3D Inspector Reflector

<!--http://doctoc.herokuapp.com/-->
- [Unity3D Inspector Reflector](#)
	- [Introduction](#)
	- [Inspection](#)
		- [Enabling the IR](#)
		- [Customization](#)
		- [Extensability](#)

## Introduction

Unity wants us to work with their Inspector in a very specific way which is rather cumbersome and error-prone in my opinion. The fact that I have abandoned many of my Unity projects left my thinking. I came to the conclusion that the default Unity Inspector doesn't allow me to write code the way I want it to be. Unity therefore prevents me from writing in a truly expressive form, making me sort of angry, and often abandoning my projects in the end.<br>
<br>
Things that make me angry:<br>
1) **The default Inspector forces us to expose fields which I often mark as private and are not part of the contract.**<br>
   Having a non-public field and exposing it by a property is often a good choice to control the setting and retrieval of a value. To display such a non-public field in the default Inspector a workaround must be put in place that displays the field (must be marked with <code>SerializeField</code>). This causes the problem that the property's getter and setter won't be called and therefore Unity's <code>OnValidate()</code> callback must be used to trigger them manually. Because we cannot tell which values have changed when <code>OnValidate()</code> is called we must validate each property and for best results only trigger the properties whose values have changed.
   
2) **There is no way to expose a property to the default Inspector.**<br>
   Based on the problem explained in (1) the solution would be to display properties rather than non-public fields.<br>
   Well there is no way of doing that without having to write a full custom inspector.
   
3) **Modification to the default Inspector forces us to write a full custom Inspector.**<br>
   Sometimes it would be beneficial to display some data in a more refined way. We may want to group some values and collapse them visually. Unity forces us to write a full custom Inspector even if we'd like to change just a small thing. A custom Inspector becomes handy if we want to completely change the appearance of the Inspector. Most of the time however we simply want to change the way a few  values are displayed.
   
## Inspection

The IR (Inspector Reflector) is an opt-in feature which means that Unity still defaults to their cumbersome Inspector after the IR is added to a project - which is good. Built in types and types from third-party providers should not be targeted by the IR. However, if a third-party type was designed with the IR in mind we won't be able to correctly inspect the type without adding the IR to our project as well. 

### Enabling the IR

In order to enable the IR for a class it has to be marked with the <code>InspectAttribute</code>.

```cs
[Inspect]
public class Enemy
{
```

Such a class' public instance properties and fields can be inspected if they are also marked with the <code>InspectAttribute</code>. Note that the Unity Serializer is only able to serialize private fields marked with the <code>SerializeFieldAttribute</code> and public fields. You therefore cannot use automatically implemented properties because the invisible private backing field wouldn't be marked with the <code>SerializeFieldAttribute</code>.

```cs
   [SerializeField]
   private int _exp;
   [Inspect]
   public int EXP
   {
      get
      {
         return _exp;
      }
      set
      {
         _exp = value;
         _level = Mathf.Floor(value / 100f);
      }
   }
   
   
   [SerializeField]
   private int _level;
   [Inspect]
   public int Level
   {
      get
      {
         return _level;
      }
   }
}
```

It is crucial to know that data can only survive a loading process if it can be serialized. Unity has some restrictions on what kinds of data can be serialized. Before using the IR you therefore want to understand all those restrictions:<br>
https://blogs.unity3d.com/2014/06/24/serialization-in-unity/

### Customization

### Extensability

Coming soon
