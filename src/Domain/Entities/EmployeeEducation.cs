using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeEducation : BaseAuditableEntity
{
    [Column("EmployeeEducationId")]

    public int EmployeeEducationId { get; set; }
    public string Degree { get; set; }
    public string Institution { get; set; }
    public int CompletionYear { get; set; }

    // Navigation property

    [ForeignKey(nameof(EmployeeProfile))]

    public int EmployeeProfileId { get; set; }
    public EmployeeProfile EmployeeProfile { get; set; }

    public List<EmployeeDocument> Documents { get; set; }
}
