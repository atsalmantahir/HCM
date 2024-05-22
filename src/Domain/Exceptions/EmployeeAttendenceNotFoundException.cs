namespace HumanResourceManagement.Domain.Exceptions;

public class EmployeeAttendenceNotFoundException : NotFoundException
{
    public EmployeeAttendenceNotFoundException(string uniqueIdentifier)
    : base($"The employee attendecne with id: {uniqueIdentifier} doesn't exist in the database.")
    {
    }
}
