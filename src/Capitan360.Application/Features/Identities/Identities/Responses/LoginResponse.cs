namespace Capitan360.Application.Features.Identities.Identities.Responses;

public class LoginResponse
{
    public string UserName { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; } = default!;
    public string SessionId { get; set; } = default!;

    public List<string> SystemPermissions { get; set; } = [];


    public string PermissionVersionControl { get; set; }
}




