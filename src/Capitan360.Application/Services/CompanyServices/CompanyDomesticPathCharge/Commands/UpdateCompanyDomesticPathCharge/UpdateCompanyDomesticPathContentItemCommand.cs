using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.UpdateCompanyDomesticPathCharge;

public record UpdateCompanyDomesticPathContentItemCommand(
    int? Id,
    int CompanyDomesticPathChargeId,
    int ContentTypeId,
    WeightType WeightType,
    long Price,
    int CompanyDomesticPathId);