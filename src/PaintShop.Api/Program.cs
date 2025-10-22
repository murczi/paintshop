using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaintShop.Domain;
using PaintShop.Domain.Interfaces;
using PaintShop.Domain.Services;
using PaintShop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("Default")
           ?? Environment.GetEnvironmentVariable("DB_CONN");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(conn!, ServerVersion.AutoDetect(conn)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.MapPost("/addUser", (IUserService userService, [FromBody] User input) =>
{
    userService.AddNewUser(input);

    return Results.Created($"/addUser", input);
});

app.MapPost("/login", (IUserService userService, [FromBody] User input) =>
{
    var result = userService.Login(input);

    if (result)
        return Results.Ok(new { Message = "Login successful" });
    else
        return Results.Unauthorized();
});

app.MapPost("/addProduct", (IProductRepository repo, [FromBody] Product input) =>
{
    repo.AddNewProduct(input);

    return Results.Created($"/addProduct", input);
});

app.MapGet("/products", (IProductRepository repo) =>
{
    var items = repo.GetAllProducts();

    return Results.Ok(items);
});

app.Run();
