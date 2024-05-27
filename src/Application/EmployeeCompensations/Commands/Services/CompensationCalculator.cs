using HumanResourceManagement.Application.EmployeeCompensations.Commands.Create;

namespace HumanResourceManagement.Application.EmployeeCompensations.Commands.Services;

public static class CompensationCalculator
{
    private static decimal CalculateGrossSalaryInternal(decimal basicSalary, decimal houseRentAllowance,
                                                        decimal medicalAllowance, decimal utilityAllowance)
    {
        return basicSalary + houseRentAllowance + medicalAllowance + utilityAllowance;
    }

    private static void ValidateBasicSalary(decimal basicSalary)
    {
        if (basicSalary <= 0)
        {
            throw new ArgumentException("Basic salary is required.");
        }
    }
}
