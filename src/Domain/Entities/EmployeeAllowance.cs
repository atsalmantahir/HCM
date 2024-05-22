using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeAllowance : BaseAuditableEntity
{
    [Column("EmployeeAllowanceId")]
    public int EmployeeAllowanceId { get; set; }
    
    [ForeignKey(nameof(EmployeeCompensation))]
    public int EmployeeCompensationId { get; set; }
    public EmployeeCompensation EmployeeCompensation { get; set; }
    
    [ForeignKey(nameof(Allowance))]
    public int AllowanceId { get; set; }
    public Allowance Allowance { get; set; }
    public decimal Amount { get; set; }
}
