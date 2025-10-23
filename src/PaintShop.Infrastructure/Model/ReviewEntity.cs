namespace PaintShop.Infrastructure.Model
{
    public class ReviewEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Desc { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}