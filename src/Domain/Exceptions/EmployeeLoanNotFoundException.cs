namespace HumanResourceManagement.Domain.Exceptions;

public class EmployeeLoanNotFoundException : NotFoundException
{
    public EmployeeLoanNotFoundException(string externalIdentifier)
    : base($"The employee loan with id: {externalIdentifier} doesn't exist in the database.")
    {
    }
}
