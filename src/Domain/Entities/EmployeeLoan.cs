using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeLoan : BaseAuditableEntity
{
    [Column("EmployeeLoanID")]
    public int EmployeeLoanID { get; set; }
    public decimal LoanAmount { get; set; }
    public LoanType LoanType { get; set; }
    public DateTime PaybackStartDate { get; set; }
    public DateTime? PaybackEndDate { get; set; }
    public string PaybackInterval { get; set; } // Monthly, One-time, etc.
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime? DisbursementDate { get; set; }
    public PaymentStatus RepaymentStatus { get; set; }

    // Foreign key
    [ForeignKey(nameof(EmployeeProfile))]
    public int EmployeeProfileId { get; set; }
    public EmployeeProfile EmployeeProfile { get; set; }
    public List<LoanGuarantor> LoanGuarantors { get; set; }
    public List<LoanApproval> LoanApprovals { get; set; }
}
