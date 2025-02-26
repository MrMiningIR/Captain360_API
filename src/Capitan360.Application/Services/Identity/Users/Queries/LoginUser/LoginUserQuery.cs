namespace Capitan360.Application.Services.Identity.Users.Queries.LoginUser;

public record  LoginUserQuery(string PhoneNumber, string Password)
{
    public string PhoneNumber { get; } = PhoneNumber;
    public string Password { get; } = Password;

}