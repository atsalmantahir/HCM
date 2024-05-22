using HumanResourceManagement.Application.Payrolls.Services.Models;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Enums;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Payrolls.Services.Implementations;

public class PayrollService : IPayrollService
{
    private readonly SemaphoreSlim currentMonthPayrollSemaphore = new SemaphoreSlim(1, 1);
    private List<Payroll> currentMonthPayroll = new List<Payroll>();

    private readonly IOrganisationsRepository organisationsRepository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;
    private readonly IPayrollsRepository payrollsRepository;
    private readonly IEmployeeAttendencesRepository employeeAttendencesRepository;
    private readonly IEmployeeCompensationsRepository employeeCompensationsRepository;
    private readonly ITaxSlabsRepository taxSlabsRepository;

    private IList<TaxSlab> taxSlabs;

    public PayrollService(
        IOrganisationsRepository organisationsRepository,
        IEmployeeProfilesRepository employeeProfilesRepository,
        IPayrollsRepository payrollsRepository,
        IEmployeeAttendencesRepository employeeAttendencesRepository,
        IEmployeeCompensationsRepository employeeCompensationsRepository,
        ITaxSlabsRepository taxSlabsRepository)
    {
        this.organisationsRepository = organisationsRepository;
        this.employeeProfilesRepository = employeeProfilesRepository;
        this.payrollsRepository = payrollsRepository;
        this.employeeAttendencesRepository = employeeAttendencesRepository;
        this.employeeCompensationsRepository = employeeCompensationsRepository;
        this.taxSlabsRepository = taxSlabsRepository;
    }

    public async Task GeneratePayrollAsync(PayrollRequest request)
    {
        var lastDateOfMonth = GetLastDateOfMonth(request.Year, request.Month);
        var organisation = await organisationsRepository.GetAsync(request.OrganisationExternalIdentifier);
        taxSlabs = taxSlabsRepository.GetAll().ToList();
        var employeesInOrganisation = organisation.Departments.SelectMany(x => x.Designations.SelectMany(x => x.EmployeeProfiles)).ToList();
        var allAttendanceRecords = employeeAttendencesRepository.GetAll();
        var currentMonthAttendances = allAttendanceRecords
            .Where(x => x.AttendanceDate.Month == lastDateOfMonth.Month && x.AttendanceDate.Year == lastDateOfMonth.Year)
            .OrderByDescending(x => x.EmployeeProfileId)
            .ThenBy(x => x.AttendanceDate)
            .ToList();

        var payrollTasks = employeesInOrganisation.Select(async employeeProfile =>
        {
            var employeeCompensation = await employeeCompensationsRepository.GetByEmployeeProfileAsync(employeeProfile.ExternalIdentifier);
            var employeeAttendances = currentMonthAttendances
                .Where(a => a.EmployeeProfileId == employeeProfile.EmployeeProfileId)
                .ToList();
            if (employeeAttendances.Count == 0)
                return;

            var workingDaysInGivenMonth = WorkingDaysCalculator.CalculateWorkingDays(request.Year, request.Month);
            var requiredHours = WorkingHoursCalculator.CalculateRequiredWorkingHours(workingDaysInGivenMonth, organisation.DailyWorkingHours);
            var totalWorkingHours = CalculateTotalWorkingHours(employeeAttendances);
            var hourlyRate = CalculateHourlyRate(employeeCompensation, requiredHours);
            var grossSalary = employeeCompensation.CurrentGrossSalary;
            var netSalary = totalWorkingHours * hourlyRate;
            var taxRate = GetTaxRate(netSalary);
            var taxDeductions = TaxDeductions(netSalary, taxRate);
            var totalEarnings = netSalary - taxDeductions;

            var existingPayroll = await payrollsRepository.GetAsync(employeeProfile.ExternalIdentifier, request.Year, request.Month);
            var payroll = new Payroll
            {
                EmployeeProfileId = employeeProfile.EmployeeProfileId,
                PayrollDate = lastDateOfMonth,
                HoursWorked = totalWorkingHours,
                GrossSalary = grossSalary,
                NetSalary = netSalary,
                Deductions = taxDeductions,
                TotalEarnings = totalEarnings,
                HourlyRate = hourlyRate,
                IncomeTaxInPercent = taxRate,
                RequiredHours = requiredHours,
                PaymentDate = lastDateOfMonth,
                AmountPaid = totalEarnings,
                PaymentMethod = PaymentMethod.None,
                PaymentStatus = PaymentStatus.Pending,
            };

            if (existingPayroll != null)
            {
                payroll.PayrollId = existingPayroll.PayrollId;
                payroll.AmountPaid = totalEarnings;
                payroll.PaymentMethod = PaymentMethod.None;
                payroll.PaymentStatus = PaymentStatus.Pending;

                await payrollsRepository.UpdateAsync(payroll, new CancellationToken());
            }
            else
            {
                await currentMonthPayrollSemaphore.WaitAsync(); // Prevent race condition
                try
                {
                    currentMonthPayroll.Add(payroll);
                }
                finally
                {
                    currentMonthPayrollSemaphore.Release();
                }
            }
        }).ToList();

        await Task.WhenAll(payrollTasks);

        if (currentMonthPayroll.Count > 0)
        {
            await payrollsRepository.InsertListAsync(currentMonthPayroll, new CancellationToken());
        }

        // For now, throw NotImplementedException to indicate that the function is not fully implemented yet
        throw new NotImplementedException();
    }

    private DateTime GetLastDateOfMonth(int year, int month)
    {
        // Calculate the number of days in the given month and year
        int daysInMonth = DateTime.DaysInMonth(year, month);

        // Create a DateTime object representing the last day of the month
        DateTime lastDayOfMonth = new DateTime(year, month, daysInMonth);

        return lastDayOfMonth;
    }

    private decimal CalculateTotalWorkingHours(List<EmployeeAttendance> attendances)
    {
        decimal totalWorkingHours = 0;
        foreach (var attendance in attendances)
        {
            decimal workingDuration = (decimal)(attendance.TimeOut - attendance.TimeIn).TotalHours;
            totalWorkingHours += workingDuration;
        }

        return totalWorkingHours;
    }

    private decimal CalculateHourlyRate(EmployeeCompensation compensation, decimal hoursRequiredInMonth)
    {
        // Calculate the total monthly compensation (excluding mode of payment)
        decimal totalMonthlyCompensation = compensation.BasicSalary +
                                           compensation.HouseRentAllowance +
                                           compensation.MedicalAllowance +
                                           compensation.UtilityAllowance;

        // Calculate the hourly rate by dividing the total monthly compensation by the hours in a month
        decimal hourlyRate = totalMonthlyCompensation / hoursRequiredInMonth;

        return hourlyRate;
    }
    private decimal TaxDeductions(decimal netSalary, decimal taxRate)
    {
        if (taxRate <= 0)
        {
            return 0;
        }

        // Calculate tax based on the applicable slab's tax rate and taxable amount
        decimal taxAmount = netSalary * (taxRate / 100);

        return taxAmount;
    }

    //private decimal GetTaxRate(decimal netSalary)
    //{
    //    // Find the applicable tax slab based on net salary
    //    var applicableSlab = taxSlabs.FirstOrDefault(slab =>
    //        netSalary >= slab.LowerLimit && netSalary <= slab.UpperLimit);

    //    if (applicableSlab == null)
    //    {
    //        // If no applicable slab is found, return zero tax
    //        return 0;
    //    }

    //    // Calculate tax based on the applicable slab's tax rate and taxable amount
    //    return applicableSlab.TaxRate;
    //}

    private decimal GetTaxRate(decimal monthlySalary)
    {
        decimal yearlySalary = monthlySalary * 12;

        var taxSlab = this.taxSlabs.FirstOrDefault(s => yearlySalary >= s.MinimumIncome && yearlySalary <= s.MaximumIncome);

        if (taxSlab != null)
        {
            var excessAmount = yearlySalary - taxSlab.MinimumIncome;
            var deduction = taxSlab.BaseTax + ((excessAmount / 100) * taxSlab.PercentageTax);
            return deduction / 12; // Convert yearly deduction to monthly
        }

        return 0;
    }
}
