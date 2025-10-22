using PaintShop.Domain;
using PaintShop.Domain.Interfaces;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }


    public IEnumerable<Product> GetAllProducts()
    {
        var productsFromDb = from p in context.Product
                             select new Product
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                                 Description = p.Description,
                                 Price = p.Price,
                                 Stock = p.Stock,
                                 IsEnabled=p.IsEnabled
                             };
        return productsFromDb;
    }
    public void AddNewProduct(Product product)
    {
        context.Product.Add(new ProductEntity
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            IsEnabled = product.IsEnabled
        });

        context.SaveChanges();
    }
}
