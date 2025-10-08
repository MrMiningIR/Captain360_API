namespace Capitan360.Application.Features.Identities.Users.UserPermissions.Commands.RemovePermissions;

public record RemovePermissionsFromUserCommand(List<int> UserPermissionIdList);
