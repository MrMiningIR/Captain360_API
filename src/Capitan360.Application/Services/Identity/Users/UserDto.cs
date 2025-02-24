namespace Capitan360.Application.Services.Identity.Users;



public class LogoutModel
{
    public string UserId { get; set; }
    public string SessionId { get; set; }
    public string Token { get; set; }
}
public class UserGroupModel
{
    public string UserId { get; set; }
    public int GroupId { get; set; }
}
