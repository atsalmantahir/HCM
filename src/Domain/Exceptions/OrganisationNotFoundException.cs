namespace HumanResourceManagement.Domain.Exceptions;

public class OrganisationNotFoundException : NotFoundException
{
    public OrganisationNotFoundException(string uniqueIdentifier)
    : base($"The organistaion with id: {uniqueIdentifier} doesn't exist in the database.")
    {
    }
}
