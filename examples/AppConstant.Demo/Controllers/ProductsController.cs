using AppConstant.Demo.Data;
using AppConstant.Demo.Models;
using AppConstant.Examples;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppConstant.Demo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Product>>> GetAllProducts([FromQuery] ProductType? type = null)
    {
        var query = _context.Products.AsQueryable();
        
        if (type is not null)
        {
            query = query.Where(x => x.Type == type);
        }
        
        return await query.ToListAsync();
    }
    
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
}