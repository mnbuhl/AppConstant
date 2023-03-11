using AppConstant.AspNetCore.Binding;
using AppConstant.AspNetCore.Json;
using AppConstant.Demo.Data;
using AppConstant.Examples;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(c =>
{
    c.ModelBinderProviders.Insert(0, new AppConstantModelBinderProvider());
}).AddJsonOptions(x => 
    x.JsonSerializerOptions.Converters.AddAppConstantConverters(typeof(ProductType).Assembly));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<ProductType>(() => new OpenApiSchema
    {
        Type = "string"
    });
});

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite("Data Source=example.db");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();