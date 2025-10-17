namespace Capitan360.Application.Features.Identities.Dtos;

public record PermissionCollectorDto(
    string DisplayName,
    string PermissionName,
    Guid PermissionCode,
    string Parent,
    Guid ParentCode,
    string SourceDisplayName);