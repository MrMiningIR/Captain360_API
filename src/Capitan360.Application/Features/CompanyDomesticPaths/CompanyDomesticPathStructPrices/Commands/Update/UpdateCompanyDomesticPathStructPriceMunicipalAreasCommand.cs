namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Update;

public record UpdateCompanyDomesticPathStructPriceMunicipalAreasCommand
{
    public List<UpdateCompanyDomesticPathStructPriceMunicipalAreasItem> DomesticPathStructPriceMunicipalAreas { get; set; } = [];
}