namespace PaintShop.Infrastructure.Model
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public byte[] Data { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}