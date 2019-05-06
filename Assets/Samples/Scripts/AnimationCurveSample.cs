using InspectorReflector;
using UnityEngine;

[EnableIR]
public class AnimationCurveSample : MonoBehaviour
{
    [Inspect]
    public AnimationCurve Field = new AnimationCurve();

    [Inspect]
    public AnimationCurve Property { get => Field; set => Field = value; }

    [Inspect]
    public AnimationCurve ReadonlyProperty { get => Field; }
}
