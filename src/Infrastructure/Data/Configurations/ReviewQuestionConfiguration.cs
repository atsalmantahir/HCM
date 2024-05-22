using HumanResourceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResourceManagement.Infrastructure.Data.Configurations;

public class ReviewQuestionConfiguration : IEntityTypeConfiguration<ReviewQuestion>
{
    public void Configure(EntityTypeBuilder<ReviewQuestion> builder)
    {
        builder.Property(e => e.ExternalIdentifier)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");
    }
}
