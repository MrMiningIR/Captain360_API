using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathCharges.Commands.Create;

public record CreateCompanyDomesticPathContentItemCommand(
    //int Id,
    int CompanyDomesticPathChargeId,
    WeightType WeightType,
    long Price,
    int ContentTypeId,
    int CompanyDomesticPathId
);