namespace Capitan360.Application.Services.Identity.Users.Queries.RefreshToken;

public record RefreshTokenQuery(string RefreshToken)
{
    public string RefreshToken { get; } = RefreshToken;


}