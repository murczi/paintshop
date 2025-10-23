using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

    public class ColorConfiguration : IEntityTypeConfiguration<ColorEntity>
    {
    public void Configure(EntityTypeBuilder<ColorEntity> builder)
    {
        builder.ToTable("Colors");
        builder.HasKey(p => p.Id);
    }
}