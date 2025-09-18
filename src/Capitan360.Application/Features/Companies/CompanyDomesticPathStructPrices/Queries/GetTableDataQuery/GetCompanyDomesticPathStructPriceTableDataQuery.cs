namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Queries.GetTableDataQuery;

public record GetCompanyDomesticPathStructPriceTableDataQuery(
    int CompanyDomesticPathId = 0,
    int PathStruct = 0,
    int BaseCost = 0

);