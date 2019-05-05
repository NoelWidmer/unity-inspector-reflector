using System;
using InspectorReflector.Implementation;

public interface IDrawer
{
    Type TargetType { get; }

    object Draw(IMemberInspectionInfo attr, object value);
}

public abstract class IDrawer<T> : IDrawer
{
    public Type TargetType => typeof(T);

    public object Draw(IMemberInspectionInfo attr, object value)
    {
        if(value is T == false)
            throw new ArgumentException($"{nameof(value)} must be of type {TargetType}.");

        return Draw(attr, (T)value);
    }

    public abstract T Draw(IMemberInspectionInfo attr, T value);
}
