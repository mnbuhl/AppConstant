using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppConstant.EntityFrameworkCore;

public class AppConstantConverter<TConst, TValue> : ValueConverter<TConst, TValue> 
    where TConst : AppConstant<TConst, TValue>, new()
    where TValue : IEquatable<TValue>, IComparable<TValue>
{
    public AppConstantConverter() : base(item => item.Value, value => AppConstant<TConst, TValue>.Get(value))
    {
    }
}