using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathCharges.Commands.Update;

public record UpdateCompanyDomesticPathChargeItemCommand(

    int CompanyDomesticPathId,
    WeightType WeightType)
{
    public int Weight { get; set; }
    public long Price { get; set; }
    public int? Id { get; set; }
    public bool ContentTypeChargeBaseNormal { get; set; }

    public UpdateCompanyDomesticPathContentItemsCommand? ContentItems { get; set; } = new();
}