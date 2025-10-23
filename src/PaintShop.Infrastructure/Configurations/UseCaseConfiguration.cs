using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

    public class UseCaseConfiguration : IEntityTypeConfiguration<UseCaseEntity>
    {
    public void Configure(EntityTypeBuilder<UseCaseEntity> builder)
    {
        builder.ToTable("UseCases");
        builder.HasKey(p => p.Id);
    }
}