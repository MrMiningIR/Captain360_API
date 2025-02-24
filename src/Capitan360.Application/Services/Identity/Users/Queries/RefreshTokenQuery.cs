namespace Capitan360.Application.Services.Identity.Users.Queries;

public record RefreshTokenQuery(string RefreshToken)
{
    public string RefreshToken { get; } = RefreshToken;


}