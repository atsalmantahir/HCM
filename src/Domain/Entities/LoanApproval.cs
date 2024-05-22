using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class LoanApproval : BaseAuditableEntity 
{
    [Column("LoanApprovalID")]
    public int LoanApprovalID { get; set; }
    public string ExternalIdentifier { get; set; }
    public string Title { get; set; }
    public string ApproverName { get; set; }
    public string ApproverDesignation { get; set; }
    public DateTime ApprovalDate { get; set; }
    public LoanApprovalStatus LoanApprovalStatus { get; set; }
    public string Reason { get; set; }
    public string Comments { get; set; }

    [ForeignKey(nameof(EmployeeLoan))]
    public int EmployeeLoanId { get; set; }
    public EmployeeLoan EmployeeLoan { get; set; } // Approval belongs to one Loan

}
