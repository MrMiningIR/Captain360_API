namespace Capitan360.Application.Features.Identities.Roles.Roles.Commands.Create;

public record CreateRoleCommand(
    string Name, 
    string PersianName,
    bool Visible);