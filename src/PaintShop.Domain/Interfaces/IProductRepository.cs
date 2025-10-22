namespace PaintShop.Domain.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    void AddNewProduct(Product product);

}