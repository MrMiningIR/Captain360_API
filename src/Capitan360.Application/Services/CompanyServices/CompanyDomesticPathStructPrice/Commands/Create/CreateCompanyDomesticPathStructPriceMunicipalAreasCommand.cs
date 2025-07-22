namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Create;

public record CreateCompanyDomesticPathStructPriceMunicipalAreasCommand
{
    public List<CreateCompanyDomesticPathStructPriceMunicipalAreasItem> DomesticPathStructPriceMunicipalAreas { get; set; } = [];
}