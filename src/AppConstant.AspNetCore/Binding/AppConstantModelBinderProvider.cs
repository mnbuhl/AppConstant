using AppConstant.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppConstant.AspNetCore.Binding;

public class AppConstantModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext? context)
    {
        if (context is null)
        {
            throw new ArgumentException(nameof(context));
        }

        var type = context.Metadata.ModelType;

        return !TypeHelper.IsDerivedFromAppConstant(type) ? null : new AppConstantModelBinder();
    }
}