using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
        .HasMaxLength(100);
        
        builder.Property(p => p.LongDesc)
        .HasMaxLength(500);
        
        builder.Property(p => p.ShortDesc)
        .HasMaxLength(100);

        builder.Property(p => p.StockAmount)
        .HasDefaultValue(true);

        builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId);

    }
}