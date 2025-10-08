namespace Capitan360.Application.Features.Identities.Roles.Roles.Commands.Update;

public record UpdateRoleCommand(
    string Name,
    string PersianName)
{
    public string Id { get; set; }
};