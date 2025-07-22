namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Update;

public record UpdateCompanyDomesticPathStructPriceMunicipalAreasCommand
{
    public List<UpdateCompanyDomesticPathStructPriceMunicipalAreasItem> DomesticPathStructPriceMunicipalAreas { get; set; } = [];
}