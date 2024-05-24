using HumanResourceManagement.Application.Payrolls.Services.Models;

namespace HumanResourceManagement.Application.Payrolls.Services;

public interface IIncomeTaxCalculator
{
    IncomeTaxResponse CalculateIncomeTax(decimal MonthlyIncome);
}
