using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Create;

public record CreateCompanyDomesticPathStructPrice(
    int CompanyDomesticPathId,
    int Weight,
    PathStructType PathStructType,
    WeightType WeightType

)
{
    public CreateCompanyDomesticPathStructPriceMunicipalAreasCommand? StructPriceArea { get; set; } = new();
};