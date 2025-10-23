using Microsoft.EntityFrameworkCore;
using PaintShop.Infrastructure.Configurations;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<ProductEntity> Product { get; set; }
    public virtual DbSet<UserEntity> User { get; set; }
    public virtual DbSet<StockEntity> Stock { get; set; }
    public virtual DbSet<UseCaseEntity> UseCase { get; set; }
    public virtual DbSet<ColorEntity> Color { get; set; }
    public virtual DbSet<BrandEntity> Brand { get; set; }
    public virtual DbSet<ImageEntity> Image { get; set; }
    public virtual DbSet<ReviewEntity> Review { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new StockConfiguration());
        builder.ApplyConfiguration(new UseCaseConfiguration());
        builder.ApplyConfiguration(new ColorConfiguration());
        builder.ApplyConfiguration(new BrandConfiguration());
        builder.ApplyConfiguration(new ImageConfiguration());
        builder.ApplyConfiguration(new ReviewConfiguration());
    }
}
