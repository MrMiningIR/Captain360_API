using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.Identity.Users.Commands.CreateUser;


public record CreateUserCommand(string FullName, string PhoneNumber, string Email, string Password, string ConfirmPassword,
    int MoadianType)
{
    public string FullName { get; } = FullName;
    public string PhoneNumber { get; } = PhoneNumber;
    public string Email { get; } = Email;
    public string Password { get; } = Password;
    public string ConfirmPassword { get; } = ConfirmPassword;
    public int MoadianFactorType { get; } = MoadianType;





}


