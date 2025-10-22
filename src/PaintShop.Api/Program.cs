using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintShop.Application.Abstractions;
using PaintShop.Domain;
using PaintShop.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("Default")
           ?? Environment.GetEnvironmentVariable("DB_CONN");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(conn!, ServerVersion.AutoDetect(conn)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.MapPost("/addProduct", (IRepository repo, [FromBody] Product input) =>
{
    repo.AddNewProduct(input);

    return Results.Created($"/addProduct", input);
});

app.MapGet("/products", (IRepository repo) =>
{
    var items = repo.GetAllProducts();

    return Results.Ok(items);
});

app.Run();
