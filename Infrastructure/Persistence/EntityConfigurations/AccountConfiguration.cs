
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sarafi.Domain.Entities;

namespace Sarafi.Infrastructure.Persistence.EntityConfigurations
{
    partial class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasMany(a => a.TransactionsIn)
                   .WithOne(t => t.FromAccount)
                   .HasForeignKey(t => t.FromAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.TransactionsOut)
                   .WithOne(t => t.ToAccount)
                   .HasForeignKey(t => t.ToAccountId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
