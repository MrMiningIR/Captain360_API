namespace Capitan360.Application.Services.Identity.Commands.AddUserToRole;

public record AddUserToRoleCommand(string UserId, string RoleId)
{
    public string UserId { get; } = UserId;
    public string RoleId { get; } = RoleId;


}