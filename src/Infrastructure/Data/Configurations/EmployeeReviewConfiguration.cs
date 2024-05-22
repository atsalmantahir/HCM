using HumanResourceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResourceManagement.Infrastructure.Data.Configurations;

public class EmployeeReviewConfiguration : IEntityTypeConfiguration<EmployeeReview>
{
    public void Configure(EntityTypeBuilder<EmployeeReview> builder)
    {
        builder.Property(e => e.ExternalIdentifier)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");
    }
}
