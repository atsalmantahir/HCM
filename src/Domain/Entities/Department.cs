using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class Department : BaseAuditableEntity
{
    [Column("DepartmentId")]
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }

    // Foreign key
    [ForeignKey(nameof(Organisation))]
    public int OrganisationId { get; set; }

    // Navigation property
    public Organisation Organisation { get; set; }

    public IList<Designation> Designations { get; set; }

    public IList<Holiday> Holidays { get; set; }
}
