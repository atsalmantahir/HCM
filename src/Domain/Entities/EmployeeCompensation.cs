using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeCompensation : BaseAuditableEntity
{
    [Column("EmployeeCompensationId")]
    public int EmployeeCompensationId { get; set; }
    public decimal BasicSalary { get; set; }
    public PaymentMethod ModeOfPayment { get; set; }

    // Navigation property

    [ForeignKey(nameof(EmployeeProfile))]
    public int EmployeeProfileId { get; set; }

    public EmployeeProfile EmployeeProfile { get; set; }

    public List<EmployeeAllowance> EmployeeAllowances { get; set; }
}
