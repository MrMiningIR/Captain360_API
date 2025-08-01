namespace Capitan360.Application.Services.Role.Commands.UpdateQuery;

public record UpdateRoleCommand(

    string RoleName,
    string RolePersianName

)
{
    public string RoleId { get; set; }
};