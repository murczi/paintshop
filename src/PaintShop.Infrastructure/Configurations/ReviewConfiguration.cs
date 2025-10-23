using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
{
    public void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        builder.ToTable("Reviews");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Desc)
        .HasMaxLength(200);

        builder.HasOne(p => p.Product)
               .WithMany(b => b.Reviews)
               .HasForeignKey(p => p.ProductId);
    }
}