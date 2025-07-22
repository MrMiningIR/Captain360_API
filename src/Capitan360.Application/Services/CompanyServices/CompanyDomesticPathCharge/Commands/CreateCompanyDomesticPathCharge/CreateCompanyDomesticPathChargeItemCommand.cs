using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.CreateCompanyDomesticPathCharge;

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