using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Queries.GetAllCompanyDomesticPaths;

public record GetAllCompanyDomesticPathsQuery(
    int CompanyId = 0,
    string? SearchPhrase = null,
    string? SortBy = null,
    int PageNumber = 1,
    int PageSize = 10,
    int? SourceCountryId = 1,
    int? SourceProvinceId=0,
    int? SourceCityId = 0,
    int? DestinationCountryId = 1,
    int? DestinationProvinceId = 0,
    int? DestinationCityId = 0,

    int Status = 0,


    SortDirection SortDirection = SortDirection.Descending);