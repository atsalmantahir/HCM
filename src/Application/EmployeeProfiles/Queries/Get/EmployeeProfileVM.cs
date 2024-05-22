
using HumanResourceManagement.Application.Departments.Queries.Get;
using HumanResourceManagement.Application.Designations.Queries.Get;
using HumanResourceManagement.Application.EmployeeCompensations.Queries.Get;
using HumanResourceManagement.Application.EmployeeEducations.Queries.Get;
using HumanResourceManagement.Application.EmployeeExperiences.Queries.Get;
using HumanResourceManagement.Domain.Enums;
using System.Text.Json.Serialization;

namespace HumanResourceManagement.Application.EmployeeProfiles.Queries.Get;

public class EmployeeProfileVM
{
    public EmployeeProfileVM()
    {
        EmployeeEducations = new List<EmployeeEducationVM>();
        EmployeeExperiences = new List<EmployeeExperienceVM>();
        Designation = new DesignationVM();
    }

    public string ExternalIdentifier { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]

    public EmployeeType EmployeeType { get; set; }
    public string LineManager { get; set; }
    public string Segment { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]

    public Gender Gender { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]

    public MaritalStatus MaritalStatus { get; set; }
    public string Contact { get; set; }
    public string EmailAddress { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EmployeeStatus ActiveStatus { get; set; }
    public DateTime? LastWorkingDayDate { get; set; }
    public DateTime? JoiningDate { get; set; }

    // Navigation properties
    public DesignationVM Designation { get; set; }

    public EmployeeCompensationVM EmployeeCompensation { get; set; }

    public List<EmployeeEducationVM> EmployeeEducations { get; set; }
    public List<EmployeeExperienceVM> EmployeeExperiences { get; set; }
}
