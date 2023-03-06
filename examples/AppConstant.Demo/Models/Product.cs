using AppConstant.Examples;

namespace AppConstant.Demo.Models;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public ProductType Type { get; set; } = ProductType.Physical;
    public int Quantity { get; set; }
    public AgeRestriction AgeRestriction { get; set; } = AgeRestriction.None;
}