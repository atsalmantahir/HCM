using HumanResourceManagement.Application.Organisations.Queries.Get;

namespace HumanResourceManagement.Application.Departments.Queries.Get;

public class DepartmentVM
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }

    public OrganisationVM Organisation { get; set; }
}
