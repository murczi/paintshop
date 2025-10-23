namespace PaintShop.Infrastructure.Model
{
    public class ProductEntity
    {
        public ProductEntity(){
            Images = new HashSet<ImageEntity>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public int UseCaseId { get; set; }
        public int ColorId { get; set; }
        public int BrandId { get; set; }
        public int Price { get; set; }
        public float Size { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<ImageEntity> Images { get; set; }
        public virtual UseCaseEntity UseCase { get; set; }
        public virtual ColorEntity Color { get; set; }
        public virtual BrandEntity Brand { get; set; }
        public virtual StockEntity Stock { get; set; }
    }
}