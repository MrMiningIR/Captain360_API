using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Create;

public record CreateCompanyDomesticPathStructPriceMunicipalAreasItem(
    //int Id,
    int CompanyDomesticPathStructPriceId,
    int MunicipalAreaId,
    WeightType WeightType,
    int Price,
    bool Static,
    int CompanyDomesticPathId,
    PathStructType PathStructType
);