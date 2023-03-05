using AppConstant.Demo.Models;
using AppConstant.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AppConstant.Demo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Product> Products => Set<Product>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddAppConstantConverters();
        base.ConfigureConventions(configurationBuilder);
    }
}