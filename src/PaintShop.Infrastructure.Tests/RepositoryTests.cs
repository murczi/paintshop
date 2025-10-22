using System;
using System.Linq;
using PaintShop.Domain;
using PaintShop.Infrastructure;
using PaintShop.Infrastructure.Tests.TestHelpers;
using Xunit;

namespace PaintShop.Infrastructure.Tests;

public class RepositoryTests
{

    [Fact]
    public void AddNewProduct_SavesToDatabase()
    {
        using var context = InMemoryDb.CreateContext();
        var repo = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 9.99f,
            Stock = 5,
            IsEnabled = true
        };

        repo.AddNewProduct(product);

        var products = repo.GetAllProducts().ToList();

        Assert.Single(products);

        var saved = products[0];
        Assert.Equal("Test Product", saved.Name);
        Assert.Equal("Test Description", saved.Description);
        Assert.Equal(9.99f, saved.Price);
        Assert.Equal(5, saved.Stock);
        Assert.True(saved.IsEnabled);
    }

    [Fact]
    public void GetAllProducts_ReturnsEmpty_WhenNoProducts()
    {
        using var context = InMemoryDb.CreateContext();
        var repo = new ProductRepository(context);

        var products = repo.GetAllProducts().ToList();

        Assert.Empty(products);
    }
}