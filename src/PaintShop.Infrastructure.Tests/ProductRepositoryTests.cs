using PaintShop.Domain;
using PaintShop.Infrastructure.Tests.TestHelpers;

namespace PaintShop.Infrastructure.Tests;

public class ProductRepositoryTests
{

    [Fact]
    public void AddNewProduct_SavesToDatabase()
    {
        using var context = InMemoryDb.CreateContext();
        var repo = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            ShortDesc = "Test Description",
            LongDesc = "Test Description",
            Price = 9999,
            IsInside = true,
            Material = "Test Material",
            ImageUrl = "Test ImageUrl",
            StockAmount = 10,
        };

        repo.AddNewProduct(product);

        var products = repo.GetAllProducts().ToList();

        Assert.Single(products);

        var saved = products[0];
        Assert.Equal("Test Product", saved.Name);
        Assert.Equal("Test Description", saved.ShortDesc);
        Assert.Equal(9999, saved.Price);
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