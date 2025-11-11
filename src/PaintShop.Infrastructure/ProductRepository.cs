using PaintShop.Domain;
using PaintShop.Domain.Interfaces;
using PaintShop.Infrastructure.Model;
using System.Linq;

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
                                 Price = p.Price,
                                 ShortDesc = p.ShortDesc,
                                 LongDesc = p.LongDesc,
                                 IsInside = p.IsInside,
                                 Material = p.Material,
                                 ImageUrl = p.ImageUrl,
                                 InStock = p.StockAmount > 0,
                             };
        return productsFromDb;
    }

    public Product GetProduct(int id)
    {

        var productFromDb = (from p in context.Product 
            where  p.Id == id
            select new Product
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ShortDesc = p.ShortDesc,
                LongDesc = p.LongDesc,
                IsInside = p.IsInside,
                Material = p.Material,
                ImageUrl = p.ImageUrl,
                InStock = p.StockAmount > 0,
            }).FirstOrDefault();
        return productFromDb;
    }
    
    public void AddNewProduct(Product product)
    {
        context.Product.Add(new ProductEntity
        {
            Name = product.Name,
            Price = product.Price,
            ShortDesc = product.ShortDesc,
            LongDesc = product.LongDesc,
            IsInside = product.IsInside,
            Material = product.Material,
            ImageUrl = product.ImageUrl,
            StockAmount = product.StockAmount,
        });

        context.SaveChanges();
    }
    
    public IEnumerable<Review> GetReviewsByProductId(int productId)
    {
        var reviewsFromDb = from p in context.Review
            select new Review
            {
                Id = p.Id,
                Stars = p.Stars,
                Desc = p.Desc,
            };
        return reviewsFromDb;
    }
    
    public void AddReviewsForProduct(Review reviewModel)
    {
        var review = new ReviewEntity
            {
                ProductId = reviewModel.ProductId,
                Stars = reviewModel.Stars,
                Desc = reviewModel.Desc,
            };
    }
}
