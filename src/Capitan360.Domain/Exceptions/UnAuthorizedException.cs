namespace Capitan360.Domain.Exceptions;

public class UnAuthorizedException : Exception
{

    public UnAuthorizedException() : base("UnAuthorized!")
    {
    }

    public UnAuthorizedException(string message) : base(message)
    {
    }



}