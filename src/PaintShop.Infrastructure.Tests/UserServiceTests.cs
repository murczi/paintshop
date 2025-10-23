using Isopoh.Cryptography.Argon2;
using PaintShop.Domain;
using PaintShop.Domain.Services;
using PaintShop.Infrastructure.Tests.TestHelpers;

namespace PaintShop.Infrastructure.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void AddNewUser_Throws_WhenEmailInUse()
        {
            using var context = InMemoryDb.CreateContext();
            var repo = new UserRepository(context);

            repo.AddNewUser(new User { Email = "dup@example.com", Password = "existing" });

            var svc = new UserService(repo);

            var newUser = new User { Email = "dup@example.com", Password = "whatever" };

            Assert.Throws<InvalidOperationException>(() => svc.AddNewUser(newUser));
        }

        [Fact]
        public void AddNewUser_CreatesUserWithHashedPassword()
        {
            using var context = InMemoryDb.CreateContext();
            var repo = new UserRepository(context);
            var svc = new UserService(repo);

            var plain = "my-plain-password";
            svc.AddNewUser(new User { Email = "newuser@example.com", Password = plain });

            var saved = repo.GetUserByEmail("newuser@example.com");

            Assert.NotNull(saved);
            Assert.Equal("newuser@example.com", saved.Email);
            Assert.NotEqual(plain, saved.Password);
            Assert.True(Argon2.Verify(saved.Password, plain));
        }

        [Fact]
        public void Login_ReturnsTrue_WithCorrectCredentials()
        {
            using var context = InMemoryDb.CreateContext();
            var repo = new UserRepository(context);
            var svc = new UserService(repo);

            var plain = "login-pass";
            svc.AddNewUser(new User { Email = "login@example.com", Password = plain });

            var loginModel = new User { Email = "login@example.com", Password = plain };

            var ok = svc.Login(loginModel);

            Assert.True(ok);
        }

        [Fact]
        public void Login_ReturnsFalse_WithWrongPassword()
        {
            using var context = InMemoryDb.CreateContext();
            var repo = new UserRepository(context);
            var svc = new UserService(repo);

            var plain = "right-pass";
            svc.AddNewUser(new User { Email = "login2@example.com", Password = plain });

            var loginModel = new User { Email = "login2@example.com", Password = "wrong-pass" };

            var ok = svc.Login(loginModel);

            Assert.False(ok);
        }

        [Fact]
        public void Login_ReturnsFalse_WhenUserNotFound()
        {
            using var context = InMemoryDb.CreateContext();
            var repo = new UserRepository(context);
            var svc = new UserService(repo);

            var loginModel = new User { Email = "doesnotexist@example.com", Password = "whatever" };

            var ok = svc.Login(loginModel);

            Assert.False(ok);
        }
    }
}