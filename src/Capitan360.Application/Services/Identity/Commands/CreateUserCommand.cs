using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.Identity.Commands;


public record CreateUserCommand(string FullName, string PhoneNumber, string Email, string Password, string ConfirmPassword,
    MoadianFactorType MoadianFactorType)
{
    public string FullName { get; } = FullName;
    public string PhoneNumber { get; } = PhoneNumber;
    public string Email { get; } = Email;
    public string Password { get; } = Password;
    public string ConfirmPassword { get; } = ConfirmPassword;
    public MoadianFactorType MoadianFactorType { get; } = MoadianFactorType;
    

}


