using HumanResourceManagement.Application.EmployeeProfiles.Queries.Get;
using HumanResourceManagement.Domain.Enums;

namespace HumanResourceManagement.Application.Payrolls.Queries.Get;

public record PayrollVM
{
    public EmployeeProfileVM EmployeeProfile { get; set; }
    public string ExternalIdentifier { get; set; }
    public DateTime PayrollDate { get; set; }
    public decimal HoursWorked { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal OvertimeHours { get; set; }
    public decimal OvertimeRate { get; set; }
    public decimal OvertimePay { get; set; }
    public decimal HolidayHours { get; set; }
    public decimal RequiredHours { get; set; }

    public decimal HolidayRate { get; set; }
    public decimal HolidayPay { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetSalary { get; set; }
    public bool HasHealthInsurance { get; set; }
    public bool HasRetirementPlan { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string RejectionReason { get; set; }
    public PaymentMethod PaymentMethod { get; set; } // e.g., Cash, Check, Bank Transfer
    public decimal AmountPaid { get; set; }

}
