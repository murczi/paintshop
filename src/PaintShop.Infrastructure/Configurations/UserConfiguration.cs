using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintShop.Infrastructure.Model;

namespace PaintShop.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("User");
        builder.HasKey(u => u.Id);

        builder.HasIndex(x => x.Email)
        .IsUnique();
        
        builder.HasMany(u=>u.Orders)
            .WithOne(o=>o.User)
            .HasForeignKey(o=>o.UserId);
    }
}