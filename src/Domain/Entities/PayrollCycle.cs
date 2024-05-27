using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class PayrollCycle : BaseAuditableEntity
{
    [Column("PayrollCycleId")]
    public int PayrollCycleId { get; set; }
    public string CycleName { get; set; } // Name of the payroll cycle (e.g., "Monthly", "Weekly", etc.)
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string CycleType { get; set; } // Monthly, Weekly, etc.
    public string Frequency { get; set; } // Every month, Every two weeks, etc.
    public string Status { get; set; } // Active, Inactive, Pending, etc.
    public List<Payroll> Payrolls { get; set; }
}
