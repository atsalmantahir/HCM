using HumanResourceManagement.Application.Common.Models;

namespace HumanResourceManagement.Application.EmployeeEducations.Queries.Get;

public class EmployeeEducationVM
{
    public int Id { get; set; }
    public EntityIdentifier EmployeeProfile { get; set; }
    public string Degree { get; set; }
    public string Institution { get; set; }
    public int CompletionYear { get; set; }
}
