using HumanResourceManagement.Application.EmployeeCompensations.Commands.Create;

namespace HumanResourceManagement.Application.EmployeeCompensations.Commands.Services;

public static class CompensationCalculator
{
    public static decimal CalculateGrossSalary(this UpdateEmployeeCompensationCommand compensation)
    {
        ValidateBasicSalary(compensation.BasicSalary);
        return CalculateGrossSalaryInternal(compensation.BasicSalary, compensation.HouseRentAllowance,
                                            compensation.MedicalAllowance, compensation.UtilityAllowance);
    }

    public static decimal CalculateGrossSalary(this CreateEmployeeCompensationCommand compensation)
    {
        ValidateBasicSalary(compensation.BasicSalary);
        return CalculateGrossSalaryInternal(compensation.BasicSalary, compensation.HouseRentAllowance,
                                            compensation.MedicalAllowance, compensation.UtilityAllowance);
    }

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
