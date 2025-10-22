using Isopoh.Cryptography.Argon2;
using PaintShop.Domain.Interfaces;

namespace PaintShop.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddNewUser(User user)
        {            
            var _user = userRepository.GetUserByEmail(user.Email);

            if (_user is not null) throw new InvalidOperationException("email in use");
            
            var newUser = new User
            {
                Email = user.Email,
                Password = Argon2.Hash(user.Password)
            };

            userRepository.AddNewUser(newUser);
        }

        public bool Login(User user)
        {
            var _user = userRepository.GetUserByEmail(user.Email);
            if (_user is null) return false;
            return Argon2.Verify(_user.Password, user.Password);
        }
    }
}