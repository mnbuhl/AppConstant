using System.Reflection;

namespace AppConstant;

public abstract class AppConstant<TConst, TValue> 
    where TConst : AppConstant<TConst, TValue>, new()
    where TValue : IComparable<TValue>, IEquatable<TValue>
{
    private TValue _value = default!;
    
    public static readonly IReadOnlyList<TConst> All;
    private static readonly Dictionary<TValue, TConst> ValueLookup = new Dictionary<TValue, TConst>();
    
    static AppConstant()
    {
        All = GetAllValues();
    }
    
    public static TConst Get(TValue value)
    {
        if (ValueLookup.TryGetValue(value, out var result))
        {
            return result;
        }

        lock (ValueLookup)
        {
            result = All.FirstOrDefault(c => c._value.Equals(value));
            
            if (result is not null)
            {
                ValueLookup.Add(value, result);
                return result;
            }
        }

        throw new ArgumentException($"No {typeof(TConst).Name} with value {value} found.");
    }
    
    protected static TConst Set(TValue value)
    {
        return new TConst
        {
            _value = value
        };
    }

    public int CompareTo(TConst other) => _value.CompareTo(other._value);
    public override string ToString() => _value.ToString();
    public override bool Equals(object? obj) => obj is AppConstant<TConst, TValue> other && other._value.Equals(_value);
    public static implicit operator TValue(AppConstant<TConst, TValue> type) => type._value;
    public static implicit operator AppConstant<TConst, TValue>(TValue value) => Set(value);
    public static implicit operator AppConstant<TConst, TValue>(TConst value) => Set(value._value);
    public static implicit operator TConst(AppConstant<TConst, TValue> value) => Set(value._value);
    public static bool operator ==(AppConstant<TConst, TValue> a, AppConstant<TConst, TValue> b) => a.Equals(b);
    public static bool operator !=(AppConstant<TConst, TValue> a, AppConstant<TConst, TValue> b) => !a.Equals(b);
    public static bool operator <(AppConstant<TConst, TValue> a, AppConstant<TConst, TValue> b) => a.CompareTo(b) < 0;
    public static bool operator >(AppConstant<TConst, TValue> a, AppConstant<TConst, TValue> b) => a.CompareTo(b) > 0;
    public static bool operator <=(AppConstant<TConst, TValue> a, AppConstant<TConst, TValue> b) => a.CompareTo(b) <= 0;
    public static bool operator >=(AppConstant<TConst, TValue> a, AppConstant<TConst, TValue> b) => a.CompareTo(b) >= 0;
    public override int GetHashCode() => _value.GetHashCode();
    
    private static List<TConst> GetAllValues()
    {
        var properties = typeof(TConst).GetProperties(BindingFlags.Public | BindingFlags.Static);
        var values = new List<TConst>(properties.Length);

        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(TConst) && property.GetGetMethod() != null)
            {
                var value = (TConst)property.GetValue(null);
                values.Add(value);
            }
        }

        return values;
    }

    internal TValue InternalGetValue()
    {
        return _value;
    }
}