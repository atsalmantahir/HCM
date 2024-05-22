using HumanResourceManagement.Application.Payrolls.Services.Models;

namespace HumanResourceManagement.Application.Payrolls.Services;

public interface IPayrollService
{
    Task<object> CalculateTaxation();
    Task GeneratePayrollAsync(PayrollRequest request);
}
