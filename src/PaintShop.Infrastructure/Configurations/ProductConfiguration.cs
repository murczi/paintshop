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
        
        builder.Property(p => p.Description)
        .HasMaxLength(100);

        builder.Property(p => p.IsEnabled)
        .HasDefaultValue(true);
    }
}