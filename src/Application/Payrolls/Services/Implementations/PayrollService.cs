using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Payrolls.Services.Models;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Enums;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Payrolls.Services.Implementations;

public class PayrollService : IPayrollService
{
    private readonly IOrganisationsRepository organisationsRepository;
    private readonly IEmployeeProfilesRepository employeeProfilesRepository;
    private readonly IPayrollCyclesRepository payrollCyclesRepository;
    private readonly IPayrollsRepository payrollsRepository;
    private readonly IEmployeeAttendencesRepository employeeAttendencesRepository;
    private readonly IEmployeeCompensationsRepository employeeCompensationsRepository;
    private readonly ITaxSlabsRepository taxSlabsRepository;
    private readonly IIncomeTaxCalculator incomeTaxCalculator;

    private IList<TaxSlab> taxSlabs;

    public PayrollService(
        IOrganisationsRepository organisationsRepository,
        IEmployeeProfilesRepository employeeProfilesRepository,
        IPayrollCyclesRepository payrollCyclesRepository,
        IPayrollsRepository payrollsRepository,
        IEmployeeAttendencesRepository employeeAttendencesRepository,
        IEmployeeCompensationsRepository employeeCompensationsRepository,
        ITaxSlabsRepository taxSlabsRepository,
        IIncomeTaxCalculator incomeTaxCalculator)
    {
        this.organisationsRepository = organisationsRepository;
        this.employeeProfilesRepository = employeeProfilesRepository;
        this.payrollCyclesRepository = payrollCyclesRepository;
        this.payrollsRepository = payrollsRepository;
        this.employeeAttendencesRepository = employeeAttendencesRepository;
        this.employeeCompensationsRepository = employeeCompensationsRepository;
        this.taxSlabsRepository = taxSlabsRepository;
        this.incomeTaxCalculator = incomeTaxCalculator;
    }

    public async Task GeneratePayrollAsync(PayrollRequest request)
    {
        var lastDateOfMonth = GetLastDateOfMonth(request.Year, request.Month);
        var organisation = await organisationsRepository.GetAsync(request.OrganisationExternalIdentifier);

        if (organisation == null)
        {
            throw new OrganisationNotFoundException(request.OrganisationExternalIdentifier);
        }

        taxSlabs = taxSlabsRepository.GetAll().ToList();
        var employeesInOrganisation = organisation.Departments.SelectMany(x => x.Designations.SelectMany(x => x.EmployeeProfiles)).ToList();
        var allAttendanceRecords = employeeAttendencesRepository.GetAll();
        var currentMonthAttendances = allAttendanceRecords
            .Where(x => x.AttendanceDate.Month == lastDateOfMonth.Month && x.AttendanceDate.Year == lastDateOfMonth.Year)
            .OrderByDescending(x => x.EmployeeProfileId)
            .ThenBy(x => x.AttendanceDate)
            .ToList();

        var payrollCycle = new PayrollCycle
        {
            StartDate = GetFirstDateOfMonth(request.Year, request.Month),
            EndDate = GetLastDateOfMonth(request.Year, request.Month),
            CycleName = "Paycycle",    
            Payrolls = new List<Payroll> { },
        };

        foreach (var employeeProfile in employeesInOrganisation)
        {
            var employeeCompensation = await employeeCompensationsRepository.GetByEmployeeProfileAsync(employeeProfile.ExternalIdentifier);

            if (employeeCompensation == null)
                continue;

            var employeeAttendances = currentMonthAttendances
                .Where(a => a.EmployeeProfileId == employeeProfile.EmployeeProfileId)
                .ToList();
            if (employeeAttendances.Count == 0)
                continue;

            var workingDaysInGivenMonth = WorkingDaysCalculator.CalculateWorkingDays(request.Year, request.Month);
            var requiredHours = WorkingHoursCalculator.CalculateRequiredWorkingHours(workingDaysInGivenMonth, organisation.DailyWorkingHours);
            var totalWorkingHours = CalculateTotalWorkingHours(employeeAttendances);
            var grossSalary = MappingExtensions.CalculateGrossSalary(employeeCompensation);
            var hourlyRate = CalculateHourlyRate(grossSalary, requiredHours);
            var netSalary = totalWorkingHours * hourlyRate;

            var taxCalculation = this.incomeTaxCalculator.CalculateIncomeTax(netSalary);

            var taxRate = taxCalculation.MonthlyTax;
            var taxDeductions = taxCalculation.MonthlyTax;
            var totalEarnings = netSalary - taxDeductions;

            var existingPayroll = await payrollsRepository.GetAsync(employeeProfile.ExternalIdentifier, request.Year, request.Month);
            var payroll = new Payroll
            {
                PayrollCycle = payrollCycle,
                EmployeeProfileId = employeeProfile.EmployeeProfileId,
                PayrollDate = lastDateOfMonth,
                HoursWorked = totalWorkingHours,
                GrossSalary = grossSalary,
                NetSalary = netSalary,
                Deductions = taxDeductions,
                TotalEarnings = totalEarnings,
                HourlyRate = hourlyRate,
                IncomeTaxInPercent = 0,
                RequiredHours = requiredHours,
                PaymentDate = lastDateOfMonth,
                AmountPaid = totalEarnings,
                PaymentMethod = PaymentMethod.None,
                PaymentStatus = PaymentStatus.Pending,
            };

            if (existingPayroll != null)
            {
                payroll.PayrollCycle = payrollCycle;

                payroll.PayrollId = existingPayroll.PayrollId;
                payroll.AmountPaid = totalEarnings;
                payroll.PaymentMethod = PaymentMethod.None;
                payroll.PaymentStatus = PaymentStatus.Pending;

                await payrollsRepository.UpdateAsync(payroll, new CancellationToken());
            }
            else
            {
                payrollCycle.Payrolls.Add(payroll);
            }

            // Update paycycle and payrolls within is left
        }

        await this.payrollCyclesRepository.InsertAsync(payrollCycle, new CancellationToken());

        // For now, throw NotImplementedException to indicate that the function is not fully implemented yet
        throw new NotImplementedException();
    }

    private DateTime GetFirstDateOfMonth(int year, int month)
    {
        DateTime lastDayOfMonth = new DateTime(year, month, 1);

        return lastDayOfMonth;
    }

    private DateTime GetLastDateOfMonth(int year, int month)
    {
        int daysInMonth = DateTime.DaysInMonth(year, month);
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

    private decimal CalculateHourlyRate(decimal grossSalary, decimal hoursRequiredInMonth)
    {
        decimal hourlyRate = grossSalary / hoursRequiredInMonth;

        return hourlyRate;
    }
}
