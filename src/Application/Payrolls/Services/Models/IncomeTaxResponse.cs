namespace HumanResourceManagement.Application.Payrolls.Services.Models;

public class IncomeTaxResponse
{
    public decimal MonthlyIncome { get; set; }
    public decimal MonthlyTax { get; set; }
    public decimal SalaryAfterTax { get; set; }
    public decimal YearlyIncome { get; set; }
    public decimal YearlyTax { get; set; }
    public decimal YearlyIncomeAfterTax { get; set; }
}
