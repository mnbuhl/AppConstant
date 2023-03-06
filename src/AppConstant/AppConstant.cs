using System.Reflection;

namespace AppConstant;

public abstract class AppConstant<TConst, TValue> 
    where TConst : AppConstant<TConst, TValue>, new()
    where TValue : IComparable<TValue>, IEquatable<TValue>
{
    public TValue Value { get; private set; } = default!;
    
    protected static TConst Set(TValue value)
    {
        return new TConst
        {
            Value = value
        };
    }

    public static TConst Get(TValue value)
    {
        try
        {
            return All.Value.First(v => v.Value.Equals(value));
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"No {typeof(TConst).Name} with value {value} found.", ex);
        }
    }

    public static Lazy<List<TConst>> All => new Lazy<List<TConst>>(GetAllValues, LazyThreadSafetyMode.ExecutionAndPublication);

    public override string ToString() => Value.ToString();
    public static implicit operator TValue(AppConstant<TConst, TValue> type) => type.Value;
    public static implicit operator AppConstant<TConst, TValue>(TValue value) => Set(value);
    public static implicit operator AppConstant<TConst, TValue>(TConst value) => Set(value.Value);
    public static implicit operator TConst(AppConstant<TConst, TValue> value) => Set(value.Value);
    public static bool operator ==(AppConstant<TConst, TValue> a, AppConstant<TConst, TValue> b) => a.Value.Equals(b.Value);
    public static bool operator !=(AppConstant<TConst, TValue> a, AppConstant<TConst, TValue> b) => !a.Value.Equals(b.Value);
    public override bool Equals(object? obj) => obj is AppConstant<TConst, TValue> other && other.Value.Equals(Value);
    public override int GetHashCode() => Value.GetHashCode();
    
    private static List<TConst> GetAllValues()
    {
        var properties = typeof(TConst).GetProperties();
        var values = new List<TConst>();
        
        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(TConst) && property.GetGetMethod() != null && property.GetGetMethod().IsStatic)
            {
                values.Add((TConst)property.GetValue(null));
            }
        }
        return values;
    }
}