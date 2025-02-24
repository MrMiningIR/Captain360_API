namespace Capitan360.Domain.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException() : base("User already exists.")
    {
    }

    public UserAlreadyExistsException(string message) : base(message)
    {
    }

    public UserAlreadyExistsException(string fullName ,string phoneNumber)
        : base($"User {fullName} with phone number '{phoneNumber}' already exists.")
    {
    }



}