namespace HumanResourceManagement.Domain.Exceptions;

public class EmployeeCompensationNotFoundException : NotFoundException
{
    public EmployeeCompensationNotFoundException(string uniqueIdentifier)
    : base($"The employee compensation with id: {uniqueIdentifier} doesn't exist in the database.")
    {
    }
}
