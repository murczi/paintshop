namespace PaintShop.Domain;

public class Product
{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int UseCaseId { get; set; }
        public int ColorId { get; set; }
        public int BrandId { get; set; }
        public int Price { get; set; }
        public bool IsEnabled { get; set; }
}
