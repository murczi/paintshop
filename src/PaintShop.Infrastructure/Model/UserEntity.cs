namespace PaintShop.Infrastructure.Model
{
    public class UserEntity
    {
        public UserEntity()
        {
            Orders = new List<OrderEntity>();
        }
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        
        public virtual ICollection<OrderEntity>  Orders { get; set; }
    }
}