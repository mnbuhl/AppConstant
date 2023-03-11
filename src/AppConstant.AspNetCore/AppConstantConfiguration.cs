using System.Reflection;
using System.Text.Json.Serialization;
using AppConstant.AspNetCore.Binding;
using AppConstant.AspNetCore.Json;
using AppConstant.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AppConstant.AspNetCore;

public class AppConstantOptions
{
    public Assembly? Assembly { get; set; }
    public bool ProjectUsingSwagger { get; set; } = true;
}

public static class AppConstantConfiguration
{
    public static IMvcBuilder AddAppConstant(this IMvcBuilder builder, Assembly assembly)
    {
        return builder.AddAppConstant(new AppConstantOptions
        {
            Assembly = assembly
        });
    }
    
    public static IMvcBuilder AddAppConstant(this IMvcBuilder builder, Action<AppConstantOptions>? options = null)
    {
        var appConstantOptions = new AppConstantOptions();
        options?.Invoke(appConstantOptions);
        
        appConstantOptions.Assembly ??= Assembly.GetCallingAssembly();

        return builder.AddAppConstant(appConstantOptions);
    }
    
    private static IMvcBuilder AddAppConstant(this IMvcBuilder builder, AppConstantOptions options)
    {
        options.Assembly ??= Assembly.GetCallingAssembly();

        var constantTypes = options.Assembly.GetTypes().GetAppConstantTypes().ToArray();

        var converters = new List<JsonConverter>();
        foreach (var constantType in constantTypes)
        {
            var converterType = typeof(AppConstantJsonValueConverter<,>)
                .MakeGenericType(constantType, constantType.BaseType!.GetGenericArguments()[1]);
            var converter = (JsonConverter)Activator.CreateInstance(converterType)!;
            converters.Add(converter);
        }

        builder.AddMvcOptions(opt => opt.ModelBinderProviders.Insert(0, new AppConstantModelBinderProvider()));

        builder.AddJsonOptions(opt =>
        {
            foreach (var converter in converters)
                opt.JsonSerializerOptions.Converters.Add(converter);
        });
        

        if (options.ProjectUsingSwagger)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                foreach (var type in constantTypes)
                {
                    c.MapType(type, () => new OpenApiSchema
                    {
                        Type = TypeHelper.GetAppConstantFromDerived(type)!.GetGenericArguments()[1].IsNumeric()
                            ? "number"
                            : "string"
                    });
                }
            });
        }
        
        return builder;
    }
}