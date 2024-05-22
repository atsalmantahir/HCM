using HumanResourceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Data.Configurations;

public class PayrollCycleConfiguration : IEntityTypeConfiguration<PayrollCycle>
{
    public void Configure(EntityTypeBuilder<PayrollCycle> builder)
    {
        builder.Property(e => e.ExternalIdentifier)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");
    }
}