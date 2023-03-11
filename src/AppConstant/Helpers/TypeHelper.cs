using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AppConstant.EntityFrameworkCore")]
[assembly: InternalsVisibleTo("AppConstant.AspNetCore")]
[assembly: InternalsVisibleTo("AppConstant.Unit")]
namespace AppConstant.Helpers;

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

    internal static bool IsNumeric(this Type type)
    {
        bool isPrimitive = type.IsPrimitive || type == typeof(decimal);
        bool isConvertible = type.GetInterfaces().Contains(typeof(IConvertible));
        bool isNumeric = isPrimitive && isConvertible;

        return isNumeric;
    }
}