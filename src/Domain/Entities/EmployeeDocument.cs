using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeDocument : BaseAuditableEntity
{
    [Column("EmployeeDocumentId")]
    public int EmployeeDocumentId { get; set; }
    public string ExternalIdentifier { get; set; }
    public DocumentType Type { get; set; }
    public string FilePath { get; set; }

    [ForeignKey(nameof(EmployeeProfile))]
    public int? EmployeeProfileId { get; set; }
    public virtual EmployeeProfile EmployeeProfile { get; set; }

    [ForeignKey(nameof(EmployeeExperience))]
    public int? EmployeeExperienceId { get; set; }
    public virtual EmployeeExperience EmployeeExperience { get; set; }

    [ForeignKey(nameof(EmployeeEducation))]
    public int? EmployeeEducationId { get; set; }
    public virtual EmployeeEducation EmployeeEducation { get; set; }
}
