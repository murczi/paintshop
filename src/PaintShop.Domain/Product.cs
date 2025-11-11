namespace PaintShop.Domain;

public class Product
{
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? ShortDesc { get; set; }
        public string? LongDesc { get; set; } 
        public bool IsInside { get; set; }
        public string Material { get; set; }
        public string ImageUrl { get; set; }
        public int StockAmount { get; set; }
        public bool InStock { get; set; }
}
