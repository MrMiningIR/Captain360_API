namespace Capitan360.Application.Services.Identity.Commands.RemoveUserFromRole;

public record RemoveUserFromRoleCommand(string UserId, string RoleId)
{
    public string UserId { get; } = UserId;
    public string RoleId { get; } = RoleId;


};