using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Queries.GetAll;

public record GetAllCompanyDomesticPathStructQuery(
    int CompanyDomesticPathId,
    int PathStruct = 0,
    string? SearchPhrase = null,
    string? SortBy = null,
    int PageNumber = 1,
    int PageSize = 15,
    SortDirection SortDirection = SortDirection.Descending);