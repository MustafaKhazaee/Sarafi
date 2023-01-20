
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sarafi.Domain.Entities;

namespace Sarafi.Infrastructure.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Mobile1).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasOne(u => u.Province)
               .WithMany(p => p.Users)
               .HasForeignKey(u => u.ProvinceId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
