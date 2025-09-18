namespace Capitan360.Application.Features.Identities.Identities.Commands.AddUserToRole;

public record AddUserToRoleCommand(string UserId, string RoleId)
{
    public string UserId { get; } = UserId;
    public string RoleId { get; } = RoleId;


}