namespace AppConstant.Extensions;

public static class TypeExtensions
{ 
    public static IEnumerable<Type> GetAppConstantTypes(this IEnumerable<Type> types)
    {
        return types.Where(t => t.IsClass && t is { IsAbstract: false, BaseType.IsGenericType: true }
                                          && t.BaseType.GetGenericTypeDefinition() == typeof(AppConstant<,>));
    }
}