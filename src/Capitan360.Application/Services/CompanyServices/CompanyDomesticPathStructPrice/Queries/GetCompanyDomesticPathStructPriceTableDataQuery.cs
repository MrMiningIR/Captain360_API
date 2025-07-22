namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Queries;

public record GetCompanyDomesticPathStructPriceTableDataQuery(
    int CompanyDomesticPathId=0,
    int PathStruct = 0,
    int BaseCost = 0

);