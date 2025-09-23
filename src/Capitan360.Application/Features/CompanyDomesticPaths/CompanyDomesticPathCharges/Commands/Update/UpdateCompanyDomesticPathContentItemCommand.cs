using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathCharges.Commands.Update;

public record UpdateCompanyDomesticPathContentItemCommand(
    int? Id,
    int CompanyDomesticPathChargeId,
    int ContentTypeId,
    WeightType WeightType,
    long Price,
    int CompanyDomesticPathId);