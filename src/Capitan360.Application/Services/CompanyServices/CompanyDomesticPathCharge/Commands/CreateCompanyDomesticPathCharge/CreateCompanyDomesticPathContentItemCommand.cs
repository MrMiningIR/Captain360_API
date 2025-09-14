using Capitan360.Domain.Enums;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.CreateCompanyDomesticPathCharge;

public record CreateCompanyDomesticPathContentItemCommand(
    //int Id,
    int CompanyDomesticPathChargeId,
    WeightType WeightType,
    long Price,
    int ContentTypeId,
    int CompanyDomesticPathId
);