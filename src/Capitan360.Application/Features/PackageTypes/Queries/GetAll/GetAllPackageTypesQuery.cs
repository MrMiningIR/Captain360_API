using System.ComponentModel;
using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.PackageTypes.Queries.GetAll;

public record GetAllPackageTypesQuery(
    [property: DefaultValue("")] string SearchPhrase = "",
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(0)] int CompanyTypeId = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);