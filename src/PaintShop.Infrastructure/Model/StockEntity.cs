namespace PaintShop.Infrastructure.Model
{
    public class StockEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}