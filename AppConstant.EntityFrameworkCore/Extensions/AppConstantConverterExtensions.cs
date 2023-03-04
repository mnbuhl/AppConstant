using AppConstant.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AppConstant.EntityFrameworkCore.Extensions;

public static class AppConstantConverterExtensions
{
    #if NET6_0_OR_GREATER

    public static void AddAppConstantConverters(this ModelConfigurationBuilder builder)
    {
        // @TODO fix this - only returning age restriction
        var propertyTypes = builder.CreateModelBuilder(null).Model.GetEntityTypes()
            .SelectMany(e => e.ClrType.GetProperties())
            .Select(p => p.PropertyType)
            .GetAppConstantTypes();

        foreach (var propertyType in propertyTypes)
        {
            var converterType = typeof(AppConstantConverter<,>)
                .MakeGenericType(propertyType, propertyType.BaseType?.GetGenericArguments()[1] ?? typeof(string));
            
            builder.Properties(propertyType).HaveConversion(converterType);
        }
    }
    
    #endif
}