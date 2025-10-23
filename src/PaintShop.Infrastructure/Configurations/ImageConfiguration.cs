using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
{
    public void Configure(EntityTypeBuilder<ImageEntity> builder)
    {
        builder.ToTable("Images");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Product)
               .WithMany(b => b.Images)
               .HasForeignKey(p => p.ProductId);
    }
}