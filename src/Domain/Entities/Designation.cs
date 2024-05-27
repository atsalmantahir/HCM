using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class Designation : BaseAuditableEntity
{
    [Column("DesignationId")]
    public int DesignationId { get; set; }
    public string DesignationName { get; set; }

    // Foreign key
    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }

    // Navigation properties
    public Department Department { get; set; }
    public IList<EmployeeProfile> EmployeeProfiles { get; set; }
}