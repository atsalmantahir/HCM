using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeExperience : BaseAuditableEntity
{
    [Column("EmployeeExperienceId")]

    public int EmployeeExperienceId { get; set; }
    public string ExternalIdentifier { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Navigation property

    [ForeignKey(nameof(EmployeeProfile))]
    public int EmployeeProfileId { get; set; }
    public EmployeeProfile EmployeeProfile { get; set; }

    public List<EmployeeDocument> Documents { get; set; }
}
