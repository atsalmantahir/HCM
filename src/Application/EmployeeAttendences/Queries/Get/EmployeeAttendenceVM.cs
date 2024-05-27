using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Entities;

namespace HumanResourceManagement.Application.EmployeeAttendences.Queries.Get;

public class EmployeeAttendenceVM
{
    public int Id { get; set; }
    public EntityIdentifier EmployeeProfile { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public TimeOnly TimeIn { get; set; }
    public TimeOnly TimeOut { get; set; }
}
