using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResourceManagement.Domain.Entities;

public class EmployeeAttendance : BaseAuditableEntity
{
    [Column("EmployeeAttendanceId")]
    public int EmployeeAttendanceId { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public TimeOnly TimeIn { get; set; }
    public TimeOnly TimeOut { get; set; }
    public bool IsApproved { get;set; }
    public int ApprovedBy { get; set; }

    [ForeignKey(nameof(EmployeeProfile))]
    public int EmployeeProfileId { get; set; }
    public EmployeeProfile EmployeeProfile { get; set; }
}
