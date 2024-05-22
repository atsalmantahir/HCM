namespace HumanResourceManagement.Domain.Exceptions;

public class EmployeeExperienceNotFoundException : NotFoundException
{
    public EmployeeExperienceNotFoundException(string uniqueIdentifier)
    : base($"The employee experience with id: {uniqueIdentifier} doesn't exist in the database.")
    {
    }
}
