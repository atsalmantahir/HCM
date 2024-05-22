﻿using HumanResourceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceManagement.Infrastructure.Data.Configurations;

public class EmployeeReviewQuestionWithAnswerConfiguration : IEntityTypeConfiguration<EmployeeReviewFromManagerWithQuestionAndAnswer>
{
    public void Configure(EntityTypeBuilder<EmployeeReviewFromManagerWithQuestionAndAnswer> builder)
    {
        builder.Property(e => e.ExternalIdentifier)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");
    }
}
