using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AppConstant.EntityFrameworkCore")]
[assembly: InternalsVisibleTo("AppConstant.AspNetCore")]
namespace AppConstant.Extensions;

public static class TypeHelper
{
    internal static IEnumerable<Type> GetAppConstantTypes(this IEnumerable<Type> types)
    {
        return types.Where(t => t.IsClass && t is { IsAbstract: false, BaseType.IsGenericType: true }
                                          && IsDerivedFromAppConstant(t));
    }

    internal static bool IsDerivedFromAppConstant(Type? type)
    {
        return GetAppConstantFromDerived(type) != null;
    }

    internal static Type? GetAppConstantFromDerived(Type? derived)
    {
        var currentType = derived?.BaseType;
        
        while (currentType != null)
        {
            if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(AppConstant<,>))
            {
                return currentType;
            }

            currentType = currentType.BaseType;
        }

        return null;
    }
}