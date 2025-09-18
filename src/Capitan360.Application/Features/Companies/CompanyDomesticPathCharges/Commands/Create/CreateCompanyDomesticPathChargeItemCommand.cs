using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Commands.Create;

public record CreateCompanyDomesticPathChargeItemCommand(
    int CompanyDomesticPathId,
    int Weight,
    long Price,

    WeightType WeightType,
    bool ContentTypeChargeBaseNormal

)
{
    public CreateCompanyDomesticPathContentItemsCommand? ContentItems { get; set; } = new();
};