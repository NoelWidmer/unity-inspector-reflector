# Unity3D Inspector Reflector

## Introduction

Unity wants us to work with their Inspector in a very specific way which is rather cumbersome and error-prone in my opinion. The fact that I have abandoned many of my Unity projects left my thinking. I came to the conclusion that the default Unity Inspector doesn't allow me to write code the way I want it to be. Unity therefore prevents me from writing in a truly expressive form, making me sort of angry, and abandoning my projects in the end.<br>
<br>
Things that make me angry:<br>
1) **The default Inspector forces us to expose fields which I often mark as private and are not part of the contract.**<br>
   Having a non-public field and exposing it by a property is often a good choice to control the setting and retrieval of a value. To display such a non-public field in the default Inspector a workaround must be put in place that displays the field (must be marked as <code>Serializeable</code>). This causes the problem that the property's getter and setter won't be called and therefore Unity's <code>OnValidate()</code> callback must be used to somehow trigger them manually. Because we cannot really tell which values have changed when <code>OnValidate()</code> is called we must validate each value and for best results only trigger the properties whos value have changed.
   
2) **There is no way to expose a property to the default Inspector.**<br>
   Based on the problem reported in (1) the solution would be to display properties rather than non-public fields.<br>
   Well there is no way of doing that without having to completely write a custom inspector.
   
3) **Modification to the default Inspector forces us to write a complete custom Inspector.**<br>
   Sometimes it would be beneficial to display some data in a more refined way. I may want to group some values and collapse them visually. In order to change the visuals of the default Inspector Unity forces us to write a custom Inspector. A custom Inspector is sort of cool thing if I wouldn't have to write the logic for the complete type I want to inspect.

## Inspection

The IR (Inspector Reflector) is an opt-in feature which means that Unity still defaults to their cumbersome Inspector after you added the IR to the project - which is good. Built in types and types from third-party providers should not be targeted by the IR. However, if a third-party type was designed with the IR in mind you won't be able to correctly inspect the type without adding the IR to your project as well. 

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
```

Unity has some more specific restrictions on what kinds of data can be serialized.<br>
Before using the IR you probably want to understand those restrictions first:<br>
https://blogs.unity3d.com/2014/06/24/serialization-in-unity/

```cs
}
```

### Customization

### Extensability

Coming soon
