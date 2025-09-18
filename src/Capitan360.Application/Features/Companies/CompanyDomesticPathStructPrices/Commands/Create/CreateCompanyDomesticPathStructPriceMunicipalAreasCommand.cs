namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Create;

public record CreateCompanyDomesticPathStructPriceMunicipalAreasCommand
{
    public List<CreateCompanyDomesticPathStructPriceMunicipalAreasItem> DomesticPathStructPriceMunicipalAreas { get; set; } = [];
}