using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class LoanPayment : BaseAuditableEntity
{
    [Column("LoanPaymentId")]

    public int LoanPaymentId { get; set; }


    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string Remarks { get; set; }

    [ForeignKey(nameof(EmployeeLoan))]
    public int EmployeeLoanId { get; set; }
    public EmployeeLoan EmployeeLoan { get; set; }
}
