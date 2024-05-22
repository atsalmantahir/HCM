using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class Payroll : BaseAuditableEntity
{
    [Column("PayrollId")]
    public int PayrollId { get; set; }
    public string ExternalIdentifier { get; set; }
    public DateTime PayrollDate { get; set; }
    public decimal HoursWorked { get; set; }
    public decimal RequiredHours { get; set; }
    public decimal OvertimeHours { get; set; }
    public decimal LateHours { get; set; }
    public decimal OvertimeRate { get; set; }
    public decimal OvertimePay { get; set; }
    public decimal HolidayHours { get; set; }
    public decimal HolidayRate { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal HolidayPay { get; set; }
    public decimal IncomeTaxInPercent { get; set; }
    public decimal Deductions { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal NetSalary { get; set; }
    public decimal TotalEarnings { get; set; }
    public bool HasHealthInsurance { get; set; }
    public bool HasRetirementPlan { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string RejectionReason { get; set; }
    public PaymentMethod PaymentMethod { get; set; } // e.g., Cash, Check, Bank Transfer
    public decimal AmountPaid { get; set; }

    [ForeignKey(nameof(PayrollCycle))]
    public int PayrollCycleId { get; set; }
    public PayrollCycle PayrollCycle { get; set; }

    [ForeignKey(nameof(EmployeeProfile))]
    public int EmployeeProfileId { get; set; }
    public EmployeeProfile EmployeeProfile { get; set; }
}
