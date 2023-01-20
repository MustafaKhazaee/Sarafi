
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sarafi.Domain.Entities;

namespace Sarafi.Infrastructure.Persistence.EntityConfigurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasOne(c => c.Province)
               .WithMany(p => p.Companies)
               .HasForeignKey(c => c.ProvinceId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
