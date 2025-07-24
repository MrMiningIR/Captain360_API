using System.ComponentModel;

namespace Capitan360.Application.Services.UserPermission.Queries.GetUserPermissions;

public record GetUserPermissionsQuery(

    [property: DefaultValue("")] string UserId,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(500)] int PageSize = 500);