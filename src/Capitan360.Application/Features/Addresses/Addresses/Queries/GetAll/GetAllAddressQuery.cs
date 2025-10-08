using System.ComponentModel;
using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Addresses.Addresses.Queries.GetAll;

public record GetAllAddressQuery(
    [property: DefaultValue("")] string SearchPhrase = "",
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(null)] int? CompanyId = null,
    [property: DefaultValue(null)] string? UserId = null,
    [property: DefaultValue(0)] int Active = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);