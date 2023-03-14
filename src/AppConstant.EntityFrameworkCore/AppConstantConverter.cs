using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppConstant.EntityFrameworkCore;

internal class AppConstantConverter<TConst, TValue> : ValueConverter<TConst, TValue> 
    where TConst : AppConstant<TConst, TValue>, new()
    where TValue : IEquatable<TValue>, IComparable<TValue>
{
    public AppConstantConverter() : base(item => item.InternalGetValue(), value => AppConstant<TConst, TValue>.Get(value))
    {
    }
    
    public AppConstantConverter(ConverterMappingHints? hints) 
        : base(item => item.InternalGetValue(), value => AppConstant<TConst, TValue>.Get(value), hints)
    {
    }
}