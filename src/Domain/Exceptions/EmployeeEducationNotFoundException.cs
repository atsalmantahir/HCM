namespace HumanResourceManagement.Domain.Exceptions;

public class EmployeeEducationNotFoundException : NotFoundException
{
    public EmployeeEducationNotFoundException(string uniqueIdentifier)
    : base($"The employee education with id: {uniqueIdentifier} doesn't exist in the database.")
    {
    }
}
