namespace PaintShop.Domain;

public class Review
{
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Stars { get; set; }
        public string Desc { get; set; }
}
