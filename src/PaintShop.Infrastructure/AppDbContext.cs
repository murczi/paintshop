using Microsoft.EntityFrameworkCore;
using PaintShop.Infrastructure.Configurations;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<ProductEntity> Product { get; set; }
    public virtual DbSet<UserEntity> User { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
    }
}
