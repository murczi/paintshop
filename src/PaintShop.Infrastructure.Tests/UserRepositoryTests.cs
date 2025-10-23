using PaintShop.Domain;
using PaintShop.Infrastructure.Tests.TestHelpers;

namespace PaintShop.Infrastructure.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void AddNewUser_SavesToDatabase()
        {
            using var context = InMemoryDb.CreateContext();
            var repo = new UserRepository(context);

            var user = new User
            {
                Email = "test@example.com",
                Password = "plain-password"
            };

            repo.AddNewUser(user);

            var saved = repo.GetUserByEmail("test@example.com");

            Assert.NotNull(saved);
            Assert.Equal("test@example.com", saved.Email);
            Assert.Equal("plain-password", saved.Password);
        }

        [Fact]
        public void GetUserByEmail_ReturnsNull_WhenNotFound()
        {
            using var context = InMemoryDb.CreateContext();
            var repo = new UserRepository(context);

            var result = repo.GetUserByEmail("noone@nowhere.test");

            Assert.Null(result);
        }
    }
}