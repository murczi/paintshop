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
                                 Description = p.Desc,
                                 Price = p.Price,
                                 IsEnabled=p.IsEnabled
                             };
        return productsFromDb;
    }
    public void AddNewProduct(Product product)
    {
        context.Product.Add(new ProductEntity
        {
            Name = product.Name,
            Desc = product.Description,
            Price = product.Price
        });

        context.SaveChanges();
    }
}
