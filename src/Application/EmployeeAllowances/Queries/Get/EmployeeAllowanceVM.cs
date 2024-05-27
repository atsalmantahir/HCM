using HumanResourceManagement.Application.Allowances.Queries.Get;

namespace HumanResourceManagement.Application.EmployeeAllowances.Queries.Get;

public record EmployeeAllowanceVM
{
    public int Id { get; set; }
    public AllowanceVM Allowance { get; set; }
    public decimal Amount { get; set; }
}
