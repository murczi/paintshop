using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Orders");
              builder.HasKey(p => p.Id);
        
              
          builder.HasOne(p=>p.User)
              .WithMany(u=>u.Orders)
              .HasForeignKey(o=>o.UserId).IsRequired();
    }
}