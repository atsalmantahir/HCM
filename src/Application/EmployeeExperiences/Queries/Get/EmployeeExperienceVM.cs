using HumanResourceManagement.Application.Common.Models;

namespace HumanResourceManagement.Application.EmployeeExperiences.Queries.Get;

public class EmployeeExperienceVM
{
    public int Id { get; set; }
    public EntityIdentifier EmployeeProfile { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
