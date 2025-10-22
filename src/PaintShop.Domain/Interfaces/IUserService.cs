namespace PaintShop.Domain.Interfaces
{
    public interface IUserService
    {
        void AddNewUser(User user);
        bool Login(User user);
    }
}