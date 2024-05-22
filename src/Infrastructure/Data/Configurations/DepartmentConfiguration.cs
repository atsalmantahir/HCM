using HumanResourceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Data.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(e => e.ExternalIdentifier)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

        builder.Property(t => t.DepartmentName)
            .HasMaxLength(200)
            .IsRequired();
    }
}
