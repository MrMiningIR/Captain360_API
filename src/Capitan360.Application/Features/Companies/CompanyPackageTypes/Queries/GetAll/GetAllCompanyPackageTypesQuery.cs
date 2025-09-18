using System.ComponentModel;
using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetAll;

public record GetAllCompanyPackageTypesQuery(
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(0)] int CompanyId = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(15)] int PageSize = 15,
    [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending);