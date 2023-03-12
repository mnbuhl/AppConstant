using System.Reflection;
using AppConstant.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppConstant.AspNetCore.Binding;

public class AppConstantModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext? bindingContext)
    {
        if (bindingContext is null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var type = bindingContext.ModelType;

        if (!TypeHelper.IsDerivedFromAppConstant(type))
        {
            throw new ArgumentException($"Type {type} is not derived from AppConstant");
        }

        var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (result == ValueProviderResult.None || result.FirstValue == null)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, result);

        var baseAppConstant = TypeHelper.GetAppConstantFromDerived(type);
        var method = baseAppConstant?.GetMethod("Get", BindingFlags.Public | BindingFlags.Static);

        if (method is null)
        {
            throw new ArgumentException($"Could not find method Get on type {type}");
        }

        try
        {
            var model = method.Invoke(null, new object[] { result.FirstValue });
            bindingContext.Result = ModelBindingResult.Success(model);
        }
        catch (TargetInvocationException)
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
                $"Value '{result.FirstValue}' not valid for AppConstant: '{type.Name}'");
        }

        return Task.CompletedTask;
    }
}