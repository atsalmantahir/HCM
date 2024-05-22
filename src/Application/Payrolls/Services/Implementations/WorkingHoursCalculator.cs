using HumanResourceManagement.Domain.Entities;

namespace HumanResourceManagement.Application.Payrolls.Services.Implementations;

public static class WorkingHoursCalculator
{
    public static decimal CalculateRequiredWorkingHours(int workingDays, decimal dailyWorkingHours)
    {
        decimal requiredWorkingHours = workingDays * dailyWorkingHours;
        return requiredWorkingHours;
    }
}
