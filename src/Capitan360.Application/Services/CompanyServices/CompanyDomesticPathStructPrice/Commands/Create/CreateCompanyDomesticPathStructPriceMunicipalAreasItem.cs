using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Create;

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