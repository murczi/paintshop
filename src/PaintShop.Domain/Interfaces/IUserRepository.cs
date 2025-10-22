namespace PaintShop.Domain.Interfaces;

public interface IUserRepository
{
    void AddNewUser(User user);
    User GetUserByEmail(string email);
}