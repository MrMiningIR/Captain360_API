namespace Capitan360.Application.Services.Identity.Users.Responses;

public record TokenResponse (string AccessToken, string RefreshToken, int ExpiresIn)
{
    public string AccessToken { get; } = AccessToken;
    public string RefreshToken { get; } = RefreshToken;
    public int ExpiresIn { get; } = ExpiresIn;
}
