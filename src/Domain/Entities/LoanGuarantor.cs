using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class LoanGuarantor : BaseAuditableEntity
{
    [Column("LoanGuarantorID")]
    public int LoanGuarantorID { get; set; }
    public string ExternalIdentifier { get; set; }
    public string Name { get; set; }
    public string Relationship { get; set; }
    public string ContactInfo { get; set; }


    [ForeignKey(nameof(EmployeeLoan))]
    public int EmployeeLoanId { get; set; }
    public EmployeeLoan EmployeeLoan { get; set; } // Guarantor can be associated with one Loan
}
