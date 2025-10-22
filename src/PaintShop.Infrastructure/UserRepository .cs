using PaintShop.Domain.Interfaces;
using PaintShop.Domain;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }


    public void AddNewUser(User user)
    {

        context.User.Add(new UserEntity
        {
            Email = user.Email,
            PasswordHash = user.Password
        });

        context.SaveChanges();
    }

    public User GetUserByEmail(string email)
    {
        var userEntity = context.User.FirstOrDefault(u => u.Email == email);
        if (userEntity == null) return null;

        return new User
        {
            Id = userEntity.Id,
            Email = userEntity.Email,
            Password = userEntity.PasswordHash
        };
    }

}