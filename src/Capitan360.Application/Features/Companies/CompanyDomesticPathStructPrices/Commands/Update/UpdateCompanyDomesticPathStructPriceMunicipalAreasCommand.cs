namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Update;

public record UpdateCompanyDomesticPathStructPriceMunicipalAreasCommand
{
    public List<UpdateCompanyDomesticPathStructPriceMunicipalAreasItem> DomesticPathStructPriceMunicipalAreas { get; set; } = [];
}