using InspectorReflector;
using UnityEngine;

[EnableIR]
public class CharacterSample : MonoBehaviour
{
    [Inspect]
    public char Field = 'x';

    [Inspect]
    public char Property { get => Field; set => Field = value; }

    [Inspect]
    public char ReadonlyProperty { get => Field; }
}
