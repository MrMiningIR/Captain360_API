using Microsoft.AspNetCore.Authorization;

namespace Capitan360.Api.PermissionAttribute
{
    public class PermissionAuthorizeAttribute(string permission) : AuthorizeAttribute, IAuthorizationRequirement
    {
        public string Permission { get; } = permission;
    }

}
