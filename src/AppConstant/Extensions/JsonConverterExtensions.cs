﻿using System.Reflection;
using AppConstant.Converters;
using System.Text.Json.Serialization;

namespace AppConstant.Extensions;

public static class JsonConverterExtensions
{
    public static void AddAppConstantConverters(this IList<JsonConverter> converters, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();
        
        var constantTypes = assembly.GetTypes().GetAppConstantTypes();

        foreach (var constantType in constantTypes)
        {
            var converterType = typeof(AppConstantJsonValueConverter<,>)
                .MakeGenericType(constantType, constantType.BaseType?.GetGenericArguments()[1]);
            var converter = (JsonConverter)Activator.CreateInstance(converterType)!;
            converters.Add(converter);
        }
    }
}