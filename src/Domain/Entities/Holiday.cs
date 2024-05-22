using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class Holiday : BaseAuditableEntity
{
    [Column("HolidayId")]
    public int HolidayId { get; set; }
    public string ExternalIdentifier { get; set; }
    public string HolidayName { get; set; }
    public DateTime HolidayDate { get; set; }
    public bool IsOfficial { get; set; }
    public bool IsActive { get; set ;}

    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}
