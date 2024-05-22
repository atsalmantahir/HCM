using HumanResourceManagement.Application.Common.Models;

namespace HumanResourceManagement.Application.EmployeeExperiences.Queries.Get;

public class EmployeeExperienceVM
{
    public string ExternalIdentifier { get; set; }
    public EntityExternalIdentifier EmployeeProfile { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
