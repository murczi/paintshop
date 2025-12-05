using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

public class FaqConfiguration : IEntityTypeConfiguration<FaqEntity>
{
    public void Configure(EntityTypeBuilder<FaqEntity> builder)
    {
        builder.ToTable("Faq");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Question)
        .HasMaxLength(200);
        
        builder.Property(p => p.Answer)
        .HasMaxLength(200);

    }
}