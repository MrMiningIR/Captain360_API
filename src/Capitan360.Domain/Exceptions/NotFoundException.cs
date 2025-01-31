namespace Capitan360.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Resource not found.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string resourceType, string resourceIdentifier)
        : base($"Resource '{resourceType}' with identifier '{resourceIdentifier}' was not found.")
    {
    }
}
