using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AppConstant.EntityFrameworkCore")]
[assembly: InternalsVisibleTo("AppConstant.AspNetCore")]
namespace AppConstant.Extensions;

public static class TypeExtensions
{
    internal static IEnumerable<Type> GetAppConstantTypes(this IEnumerable<Type> types)
    {
        return types.Where(t => t.IsClass && t is { IsAbstract: false, BaseType.IsGenericType: true }
                                          && IsDerivedFromAppConstant(t));
    }

    private static bool IsDerivedFromAppConstant(Type? type)
    {
        var currentType = type?.BaseType;
        
        while (currentType != null)
        {
            if (currentType.IsGenericType && currentType.GetGenericTypeDefinition() == typeof(AppConstant<,>))
            {
                return true;
            }

            currentType = currentType.BaseType;
        }

        return false;
    }
}