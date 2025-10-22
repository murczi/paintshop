namespace PaintShop.Infrastructure.Model
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }
}