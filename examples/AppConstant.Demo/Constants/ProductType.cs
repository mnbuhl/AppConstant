namespace AppConstant.Demo.Constants;

public sealed class ProductType : AppConstant<ProductType, string>
{
    public static ProductType Physical => Set("physical");
    public static ProductType Digital => Set("digital");
    public static ProductType Service => Set("service");
}