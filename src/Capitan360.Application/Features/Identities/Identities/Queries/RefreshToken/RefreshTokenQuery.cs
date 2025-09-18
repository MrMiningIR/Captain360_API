namespace Capitan360.Application.Features.Identities.Identities.Queries.RefreshToken;

public record RefreshTokenQuery(string RefreshToken)
{
    public string RefreshToken { get; } = RefreshToken;


}