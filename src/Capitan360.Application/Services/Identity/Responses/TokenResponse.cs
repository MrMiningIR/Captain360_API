namespace Capitan360.Application.Services.Identity.Responses;

public record TokenResponse(string AccessToken, string RefreshToken, string PermissionVersionControl, int ExpiresIn)
{
    public string AccessToken { get; } = AccessToken;
    public string RefreshToken { get; } = RefreshToken;
    public string PermissionVersionControl { get; } = PermissionVersionControl;
    public int ExpiresIn { get; } = ExpiresIn;
}
