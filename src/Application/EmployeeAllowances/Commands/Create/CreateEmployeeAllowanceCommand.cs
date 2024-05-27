using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.EmployeeCompensations.Commands.Create;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.EmployeeAllowances.Commands.Create;

public record CreateEmployeeAllowanceCommand : IRequest<CreateEmployeeAllowanceCommand>
{
    public EntityIdentifier Allowance { get; set; }

    public decimal Amount { get; set; }
}
