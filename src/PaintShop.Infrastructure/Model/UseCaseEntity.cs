namespace PaintShop.Infrastructure.Model
{
    public class UseCaseEntity
    {
        public UseCaseEntity(){
            Products = new HashSet<ProductEntity>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}