namespace Capitan360.Application.Features.Identities.Users.UserPermissions.Commands.AssignPermissions;

public record AssignPermissionsToUserCommand(
    List<int> PermissionList,
    string UserId);