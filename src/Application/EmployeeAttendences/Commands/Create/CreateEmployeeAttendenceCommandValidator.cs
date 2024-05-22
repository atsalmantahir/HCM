using HumanResourceManagement.Application.Designations.Commands;
using HumanResourceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResourceManagement.Application.EmployeeAttendences.Commands.Create;

public class CreateEmployeeAttendenceCommandValidator : AbstractValidator<CreateEmployeeAttendenceCommand>
{
    public CreateEmployeeAttendenceCommandValidator()
    {

    }
}
