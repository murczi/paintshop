namespace PaintShop.Domain.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    Product GetProduct(int id);
    void AddNewProduct(Product product);
    void AddReviewsForProduct(Review reviewModel);
    IEnumerable<Review> GetReviewsByProductId(int productId);


}