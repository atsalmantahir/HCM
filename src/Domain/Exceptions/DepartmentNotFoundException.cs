namespace HumanResourceManagement.Domain.Exceptions;

public class DepartmentNotFoundException : NotFoundException
{
    public DepartmentNotFoundException(string uniqueIdentifier)
    : base($"The department with id: {uniqueIdentifier} doesn't exist in the database.")
    {
    }
}
