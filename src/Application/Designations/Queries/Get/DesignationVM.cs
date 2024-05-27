using HumanResourceManagement.Application.Departments.Queries.Get;

namespace HumanResourceManagement.Application.Designations.Queries.Get;

public class DesignationVM
{
    public int Id { get; set; }
    public string DesignationName { get; set; }

    public DepartmentVM Department { get; set; }
}
