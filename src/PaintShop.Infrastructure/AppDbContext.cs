using Microsoft.EntityFrameworkCore;
using PaintShop.Infrastructure.Configurations;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<ProductEntity> Product { get; set; }
    public virtual DbSet<UserEntity> User { get; set; }
    public virtual DbSet<ReviewEntity> Review { get; set; }
    public virtual DbSet<OrderEntity> Order { get; set; }
    public virtual DbSet<ProductsToOrderEntity> ProductsToOrder { get; set; }
    public virtual DbSet<FaqEntity> Faq { get; set; }
    public virtual DbSet<CouponEntity> Coupon { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new ReviewConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new ProductsToOrderConfiguration());
        builder.ApplyConfiguration(new FaqConfiguration());
        builder.ApplyConfiguration(new CouponConfiguration());
    }
}
