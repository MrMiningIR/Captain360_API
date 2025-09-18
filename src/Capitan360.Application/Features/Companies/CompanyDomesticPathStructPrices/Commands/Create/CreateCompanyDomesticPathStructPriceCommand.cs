using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Create;

public record CreateCompanyDomesticPathStructPriceCommand(
    int CompanyDomesticPathId,
    int Weight,
    WeightType WeightType,
    PathStructType PathStructType);








