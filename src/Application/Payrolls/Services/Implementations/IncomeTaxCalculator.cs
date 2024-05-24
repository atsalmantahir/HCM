using HumanResourceManagement.Application.Payrolls.Services.Models;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Payrolls.Services.Implementations;

public class IncomeTaxCalculator : IIncomeTaxCalculator
{
    private readonly ITaxSlabsRepository taxSlabsRepository;
    private IList<TaxSlab> taxSlabs;

    public IncomeTaxCalculator(ITaxSlabsRepository taxSlabsRepository)
    {
        this.taxSlabsRepository = taxSlabsRepository;
        taxSlabs = this.taxSlabsRepository.GetAll().ToList();
    }
    public IncomeTaxResponse CalculateIncomeTax(decimal monthlyIncome)
    {
        decimal yearlySalary = monthlyIncome * 12;

        var taxSlab = this.taxSlabs
            .FirstOrDefault(s => yearlySalary >= s.MinimumIncome && yearlySalary <= s.MaximumIncome);

        if (taxSlab != null)
        {
            var excessAmount = yearlySalary - taxSlab.MinimumIncome;
            var deduction = taxSlab.BaseTax + ((excessAmount / 100) * taxSlab.PercentageTax);

            var incomeTaxResponse = new IncomeTaxResponse
            {
                MonthlyIncome = monthlyIncome,
                MonthlyTax = deduction / 12,
                SalaryAfterTax = monthlyIncome - (deduction / 12),
                YearlyIncome = yearlySalary,
                YearlyTax = deduction,
                YearlyIncomeAfterTax = yearlySalary - deduction
            };
            return incomeTaxResponse;
        }

        return null;
    }

}
