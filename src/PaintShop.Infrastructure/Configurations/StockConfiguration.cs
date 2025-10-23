using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<StockEntity>
{
    public void Configure(EntityTypeBuilder<StockEntity> builder)
    {
        builder.ToTable("Stocks");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Product)
               .WithOne(b => b.Stock)
               .HasForeignKey<StockEntity>(p => p.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}