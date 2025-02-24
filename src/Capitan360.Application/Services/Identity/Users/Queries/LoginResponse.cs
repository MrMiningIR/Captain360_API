namespace Capitan360.Application.Services.Identity.Users.Queries;

public class LoginResponse
{
    public string AccessToken { get; set; } = default!;
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; } = default!;
    public string SessionId { get; set; } = default!;
}