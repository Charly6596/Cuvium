using UnityEngine;

[CreateAssetMenu(
    fileName = "LongClampedVariable.asset",
    menuName = SOArchitecture_Utility.VARIABLE_CLAMPED_SUBMENU + "long",
    order = 120)]
public class LongClampedVariable : LongVariable, IClampedVariable<long, LongVariable, LongReference>
{
    public LongReference MinValue { get { return _minClampedValue; } }
    public LongReference MaxValue { get { return _maxClampedValue; } }

    [SerializeField]
    private LongReference _minClampedValue = default(LongReference);
    [SerializeField]
    private LongReference _maxClampedValue = default(LongReference);

    public virtual long ClampValue(long value)
    {
        if (value.CompareTo(MinValue.Value) < 0)
        {
            return MinValue.Value;
        }            
        else if (value.CompareTo(MaxValue.Value) > 0)
        {
            return MaxValue.Value;
        }            
        else
        {
            return value;
        }            
    }
    public override long SetValue(BaseVariable<long> value)
    {
        return ClampValue(value.Value);
    }
    public override long SetValue(long value)
    {
        return ClampValue(value);
    }
}