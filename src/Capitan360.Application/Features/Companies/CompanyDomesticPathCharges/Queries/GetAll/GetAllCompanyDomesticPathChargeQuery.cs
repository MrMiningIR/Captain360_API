using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Queries.GetAll;

public record GetAllCompanyDomesticPathChargeQuery(
    int CompanyDomesticPathId,
    string? SearchPhrase = null,
    string? SortBy = null,
    int PageNumber = 1,
    int PageSize = 15,
    SortDirection SortDirection = SortDirection.Descending);