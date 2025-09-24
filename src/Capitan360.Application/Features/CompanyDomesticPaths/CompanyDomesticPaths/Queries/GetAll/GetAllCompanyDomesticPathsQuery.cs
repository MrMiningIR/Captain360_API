using System.ComponentModel;
using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetAll;

public record GetAllCompanyDomesticPathsQuery(
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(0)] int CompanyId = 0,
    [property: DefaultValue(0)] int Active = 0,
    [property: DefaultValue(0)] int SourceCityId = 0,
    [property: DefaultValue(0)] int DestinationCityId = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(15)] int PageSize = 15,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);