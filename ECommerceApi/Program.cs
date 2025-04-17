using ECommerceApi.Data;
using ECommerceApi.Data.Repositories;
using ECommerceApi.Data.Repositories.Interfaces;
using ECommerceApi.Services;
using ECommerceApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<ECommerceDbContext>(o =>
        o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseStatusCodePages();


app.Run();