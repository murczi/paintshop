using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

    public class BrandConfiguration : IEntityTypeConfiguration<BrandEntity>
    {
    public void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        builder.ToTable("Brands");
        builder.HasKey(p => p.Id);
    }
}