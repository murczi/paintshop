using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

public class ProductsToOrderConfiguration : IEntityTypeConfiguration<ProductsToOrderEntity>
{
    public void Configure(EntityTypeBuilder<ProductsToOrderEntity> builder)
    {
        builder.ToTable("ProductsToOrder");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Product)
               .WithMany(b => b.ProductsToOrder)
               .HasForeignKey(p => p.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
               
        builder.HasOne(p => p.Order)
               .WithMany(b => b.ProductsToOrder)
               .HasForeignKey(p => p.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
