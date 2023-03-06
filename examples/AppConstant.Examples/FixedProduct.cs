namespace AppConstant.Examples;

public sealed class FixedProduct : AppConstant<FixedProduct, Guid>
{
    public static FixedProduct Fee => Set(Guid.Parse("edafae31-be28-44d6-a628-8bfc3442810f"));
    public static FixedProduct Tax => Set(Guid.Parse("ac2efa8a-928b-4846-85f9-be6b22340c3b"));
    public static FixedProduct Shipping => Set(Guid.Parse("6a7b0abd-2a84-4ca2-bbbf-74f6fbc65047"));
    public static FixedProduct Discount => Set(Guid.Parse("55a207d1-93b1-4fdb-bf5f-985e21c64195"));
    public static FixedProduct Refund => Set(Guid.Parse("2c9922c4-f085-4ddf-8627-6a6b8a452f10"));
}