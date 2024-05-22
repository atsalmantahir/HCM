using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Domain.Entities;

namespace HumanResourceManagement.Application.EmployeeAttendences.Queries.Get;

public class EmployeeAttendenceVM
{
    public string ExternalIdentifier { get; set; }
    public EntityExternalIdentifier EmployeeProfile { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public TimeOnly TimeIn { get; set; }
    public TimeOnly TimeOut { get; set; }
}
