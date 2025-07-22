using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Queries;

public record GetAllCompanyDomesticPathStructQuery(
    int CompanyDomesticPathId,
    int PathStruct = 0,
    string? SearchPhrase = null,
    string? SortBy = null,
    int PageNumber = 1,
    int PageSize = 15,
    SortDirection SortDirection = SortDirection.Descending);