using System.Reflection;

namespace AppConstant;

public abstract class AppConstant<TConst, TValue>
    where TConst : AppConstant<TConst, TValue>, new()
    where TValue : IComparable<TValue>, IEquatable<TValue>
{
    private TValue _value = default!;

    public static readonly Lazy<IReadOnlyList<TConst>> All =
        new Lazy<IReadOnlyList<TConst>>(GetAllValues, LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<TValue, TConst>> ValueLookup =
        new Lazy<Dictionary<TValue, TConst>>(LazyThreadSafetyMode.ExecutionAndPublication);

    public static TConst Get(TValue value)
    {
        if (ValueLookup.Value.TryGetValue(value, out var result))
        {
            return result;
        }

        lock (ValueLookup)
        {
            result = All.Value.FirstOrDefault(c => c._value.Equals(value));

            if (result is not null)
            {
                ValueLookup.Value.Add(value, result);
                return result;
            }
        }

        throw new ArgumentException($"No '{typeof(TConst).Name}' with value '{value}' found.");
    }

    public static bool TryGetValue(TValue value, out TConst result)
    {
        var found = false;

        try
        {
            result = Get(value);
            found = true;
        }
        catch (ArgumentException)
        {
            result = default!;
            return found;
        }

        return found;
    }

    protected static TConst Set(TValue value)
    {
        return new TConst
        {
            _value = value
        };
    }

    public int CompareTo(TConst other) => _value.CompareTo(other._value);
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
    public override string ToString() => _value.ToString();


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