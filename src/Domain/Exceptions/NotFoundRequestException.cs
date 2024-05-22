namespace HumanResourceManagement.Domain.Exceptions;

public class NotFoundRequestException : Exception
{
    public NotFoundRequestException(string message) : base(message) { }
}

