using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Create;

public record CreateCompanyDomesticPathStructPrice(
    int CompanyDomesticPathId,
    int Weight,
    PathStructType PathStructType,
    WeightType WeightType

)
{
    public CreateCompanyDomesticPathStructPriceMunicipalAreasCommand? StructPriceArea { get; set; } = new();
};