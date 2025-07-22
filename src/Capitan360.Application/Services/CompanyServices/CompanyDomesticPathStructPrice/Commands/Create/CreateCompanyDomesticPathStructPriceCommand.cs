using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Create;

public record CreateCompanyDomesticPathStructPriceCommand(
    int CompanyDomesticPathId,
    int Weight,
    WeightType WeightType,
    PathStructType PathStructType);








