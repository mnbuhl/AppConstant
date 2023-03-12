using AppConstant.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AppConstant.EntityFrameworkCore.Extensions;

public static class AppConstantConverterExtensions
{
    public static void AddAppConstantConverters(this ModelConfigurationBuilder builder)
    {
        var propertyTypes = builder.CreateModelBuilder(null).Model.GetEntityTypes()
            .SelectMany(x => x.ClrType.GetProperties())
            .Select(p => p.PropertyType)
            .Distinct()
            .GetAppConstantTypes();

        foreach (var propertyType in propertyTypes)
        {
            var converterType = typeof(AppConstantConverter<,>)
                .MakeGenericType(propertyType, propertyType.BaseType?.GetGenericArguments()[1] ?? typeof(string));
            
            builder.Properties(propertyType).HaveConversion(converterType);
        }
    }
}