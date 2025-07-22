namespace Capitan360.Application.Services.Identity.Queries.RefreshToken;

public record RefreshTokenQuery(string RefreshToken)
{
    public string RefreshToken { get; } = RefreshToken;


}