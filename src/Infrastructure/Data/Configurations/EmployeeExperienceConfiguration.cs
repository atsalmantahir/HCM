using HumanResourceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceManagement.Infrastructure.Data.Configurations;

public class EmployeeExperienceConfiguration : IEntityTypeConfiguration<EmployeeExperience>
{
    public void Configure(EntityTypeBuilder<EmployeeExperience> builder)
    {
        
    }
}
