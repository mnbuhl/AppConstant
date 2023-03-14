using System.Reflection;

namespace AppConstant;

/// <summary>
/// Base type for creating type safe constants 
/// </summary>
/// <typeparam name="TConst">Constant inheriting from this</typeparam>
/// <typeparam name="TValue">Type of the constant value</typeparam>
public abstract class AppConstant<TConst, TValue>
    where TConst : AppConstant<TConst, TValue>, new()
    where TValue : IComparable<TValue>, IEquatable<TValue>
{
    private TValue _value = default!;

    private static readonly Lazy<IReadOnlyList<TConst>> AllConstants =
        new Lazy<IReadOnlyList<TConst>>(GetAllValues, LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<TValue, TConst>> ValueLookup =
        new Lazy<Dictionary<TValue, TConst>>(LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Gets the value of the constant.
    /// </summary>
    /// <param name="value">Value to get constant from</param>
    /// <returns>Constant from value</returns>
    /// <exception cref="ArgumentException">Thrown if no constant with the given value exists.</exception>
    public static TConst Get(TValue value)
    {
        if (ValueLookup.Value.TryGetValue(value, out var result))
        {
            return result;
        }

        lock (ValueLookup)
        {
            result = AllConstants.Value.FirstOrDefault(c => c._value.Equals(value));

            if (result is not null)
            {
                ValueLookup.Value.Add(value, result);
                return result;
            }
        }

        throw new ArgumentException($"No '{typeof(TConst).Name}' with value '{value}' found.");
    }
    
    /// <summary>
    /// Gets all values of the constant type.
    /// </summary>
    public static IReadOnlyList<TConst> All() => AllConstants.Value;

    /// <summary>
    /// Try to get the constant from the given value.
    /// </summary>
    /// <param name="value">Value trying to get constant from</param>
    /// <param name="result">AppConstant from value if found, null otherwise</param>
    /// <returns>True if AppConstant is found, false otherwise</returns>
    public static bool TryGetFromValue(TValue value, out TConst? result)
    {
        bool found = false;

        try
        {
            result = Get(value);
            found = true;
        }
        catch (ArgumentException)
        {
            result = null;
            return found;
        }

        return found;
    }

    /// <summary>
    /// Sets the value of the constant.
    /// </summary>
    /// <param name="value">Value to set</param>
    /// <returns>Constant with the given value</returns>
    protected static TConst Set(TValue value)
    {
        return new TConst
        {
            _value = value
        };
    }

    /// <summary>
    /// Compares the value of the constant to the value of another constant.
    /// </summary>
    /// <param name="other">Constant to compare to</param>
    /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
    /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="other" /> in the sort order.</description></item><item><term> Zero</term><description> This instance occurs in the same position in the sort order as <paramref name="other" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="other" /> in the sort order.</description></item></list></returns>
    public int CompareTo(TConst other) => _value.CompareTo(other._value);
    
    /// <summary>
    /// Compares the value of the constant to the value of another constant.
    /// </summary>
    /// <param name="obj">Constant to compare to</param>
    /// <returns>True if the values are equal, false otherwise</returns>
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
    
    /// <summary>
    /// Returns the string representation of the constant value.
    /// </summary>
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