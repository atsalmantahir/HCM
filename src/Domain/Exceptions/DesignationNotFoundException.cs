namespace HumanResourceManagement.Domain.Exceptions;

public class DesignationNotFoundException : NotFoundException
{
    public DesignationNotFoundException(string uniqueIdentifier)
    : base($"The designation with id: {uniqueIdentifier} doesn't exist in the database.")
    {
    }
}
