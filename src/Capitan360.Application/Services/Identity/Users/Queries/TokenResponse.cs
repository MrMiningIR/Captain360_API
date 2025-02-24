namespace Capitan360.Application.Services.Identity.Users.Queries;

public record TokenResponse (string AccessToken, string RefreshToken, int ExpiresIn)
{
    public string AccessToken { get; } = AccessToken;
    public string RefreshToken { get; } = RefreshToken;
    public int ExpiresIn { get; } = ExpiresIn;
}
