namespace HumanResourceManagement.Domain.Exceptions;

public class ConflictRequestException : Exception
{
    public ConflictRequestException(string message) : base(message) { }
}
