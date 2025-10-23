namespace PaintShop.Infrastructure.Model
{
    public class OrderEntity
    {
        public OrderEntity()
        {
            ProductsToOrder = new HashSet<ProductsToOrderEntity>();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string DeliveryType { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public bool Paid { get; set; }
        public virtual ICollection<ProductsToOrderEntity> ProductsToOrder { get; set; }
    }
}