namespace HumanResourceManagement.Domain.Exceptions;

public sealed class EmployeeNotFoundException : NotFoundException
{
    public EmployeeNotFoundException(string employeeUniqueIdentifier)
    : base($"The employee with id: {employeeUniqueIdentifier} doesn't exist in the database.")
    { }

}
