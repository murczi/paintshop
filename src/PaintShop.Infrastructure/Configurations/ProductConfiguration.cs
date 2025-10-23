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
        
        builder.Property(p => p.Desc)
        .HasMaxLength(100);

        builder.Property(p => p.IsEnabled)
        .HasDefaultValue(true);

        builder.HasOne(p => p.UseCase)
               .WithMany(b => b.Products)
               .HasForeignKey(p => p.UseCaseId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Color)
               .WithMany(b => b.Products)
               .HasForeignKey(p => p.ColorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Brand)
               .WithMany(b => b.Products)
               .HasForeignKey(p => p.BrandId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}