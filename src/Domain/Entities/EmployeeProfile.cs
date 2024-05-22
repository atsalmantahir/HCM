using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeProfile : BaseAuditableEntity
{
    [Column("EmployeeProfileId")]

    public int EmployeeProfileId { get; set; }
    public string ExternalIdentifier { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public EmployeeType EmployeeType { get; set; }
    public string LineManager { get; set; }
    public string Segment { get; set; }
    public Gender Gender { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string Contact { get; set; }
    public string EmailAddress { get; set; }
    public EmployeeStatus ActiveStatus { get; set; }
    public DateTime? LastWorkingDayDate { get; set; }
    public DateTime? JoiningDate { get; set; }


    // Foreign key

    [ForeignKey(nameof(Designation))]

    public int DesignationId { get; set; }

    // Navigation properties
    public Designation Designation { get; set; }

    public EmployeeCompensation EmployeeCompensation { get; set; }
    public List<EmployeeEducation> EmployeeEducations { get; set; }
    public List<EmployeeExperience> EmployeeExperiences { get; set; }
    public List<EmployeeLoan> EmployeeLoans { get; set; }

    public List<EmployeeDocument> Documents { get; set; }
}
