using HumanResourceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Data.Configurations;

public class AllowanceConfiguration : IEntityTypeConfiguration<Allowance>
{
    public void Configure(EntityTypeBuilder<Allowance> builder)
    {
        
    }
}
