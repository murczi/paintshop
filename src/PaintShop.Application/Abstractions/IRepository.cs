using PaintShop.Domain;

namespace PaintShop.Application.Abstractions;

public interface IRepository
{
    IEnumerable<Product> GetAllProducts();
    void AddNewProduct(Product product);

}