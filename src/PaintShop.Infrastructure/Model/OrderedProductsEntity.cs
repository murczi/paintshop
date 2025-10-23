namespace PaintShop.Infrastructure.Model
{
    public class ProductsToOrderEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public virtual ProductEntity Product { get; set; }
        public virtual OrderEntity Order { get; set; }
    }
}