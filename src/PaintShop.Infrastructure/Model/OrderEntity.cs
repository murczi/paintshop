namespace PaintShop.Infrastructure.Model
{
    public class OrderEntity
    {
        public OrderEntity()
        {
            ProductsToOrder = new HashSet<ProductsToOrderEntity>();
        }
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public bool Paid { get; set; }
        public virtual ICollection<ProductsToOrderEntity> ProductsToOrder { get; set; }
        public virtual UserEntity User { get; set; }
    }
}