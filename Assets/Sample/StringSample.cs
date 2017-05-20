 using UnityEngine;

[Inspect]
public class StringSample : MonoBehaviour
{
    [InspectString(StringInspectionType.Field)]
    public string Field
    {
        get; set;
    }

    [InspectString(StringInspectionType.DelayedField)]
    public string DelayedField
    {
        get; set;
    }

    [InspectString(StringInspectionType.Tag)]
    public string Tag
    {
        get; set;
    }

    [InspectString(StringInspectionType.Area)]
    public string Area
    {
        get; set;
    }
}