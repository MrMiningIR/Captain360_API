namespace Capitan360.Application.Features.Permission.Dtos;

public record PermissionCollectorDto(
    string DisplayName,
    string PermissionName,
    Guid PermissionCode,
    string Parent,
    Guid ParentCode,
    string SourceDisplayName);