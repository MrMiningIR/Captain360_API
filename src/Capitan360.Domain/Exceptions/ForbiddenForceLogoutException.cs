namespace Capitan360.Domain.Exceptions;


public class ForbiddenForceLogoutException : Exception
{
    public ForbiddenForceLogoutException() : base("Forbidden!")
    {
    }

    public ForbiddenForceLogoutException(string message) : base(message)
    {
    }

}