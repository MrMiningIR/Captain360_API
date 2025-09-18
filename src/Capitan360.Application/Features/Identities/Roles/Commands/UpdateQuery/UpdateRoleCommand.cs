namespace Capitan360.Application.Features.Role.Commands.UpdateQuery;

public record UpdateRoleCommand(

    string RoleName,
    string RolePersianName

)
{
    public string RoleId { get; set; }
};