
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sarafi.Domain.Entities;

namespace Sarafi.Infrastructure.Persistence.EntityConfigurations;

public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder.HasOne(c => c.FromUser)
               .WithMany(u => u.ConnectionsFrom)
               .HasForeignKey(u => u.FromUserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.ToUser)
               .WithMany(u => u.ConnectionsTo)
               .HasForeignKey(u => u.ToUserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
