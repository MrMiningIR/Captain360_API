using Microsoft.AspNetCore.Authorization;

namespace Capitan360.Infrastructure.Authorization.Requirements;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}