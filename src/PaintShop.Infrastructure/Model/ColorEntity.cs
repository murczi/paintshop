namespace PaintShop.Infrastructure.Model
{
    public class ColorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}