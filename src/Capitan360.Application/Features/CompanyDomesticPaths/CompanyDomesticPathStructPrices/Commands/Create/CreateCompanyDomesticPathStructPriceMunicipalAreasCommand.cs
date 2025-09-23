namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Create;

public record CreateCompanyDomesticPathStructPriceMunicipalAreasCommand
{
    public List<CreateCompanyDomesticPathStructPriceMunicipalAreasItem> DomesticPathStructPriceMunicipalAreas { get; set; } = [];
}