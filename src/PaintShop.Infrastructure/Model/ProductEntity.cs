namespace PaintShop.Infrastructure.Model
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public bool IsEnabled { get; set; }
    }
}