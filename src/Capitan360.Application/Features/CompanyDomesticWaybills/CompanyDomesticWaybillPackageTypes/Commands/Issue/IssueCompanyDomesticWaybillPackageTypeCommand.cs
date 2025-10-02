namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.Issue;

public class IssueCompanyDomesticWaybillPackageTypeCommand
{
    public int CompanyDomesticWaybillId { get; set; }
    public int CompanyPackageTypeId { get; set; }
    public int CompanyContentTypeId { get; set; }
    public string UserInsertedContentName { get; set; } = default!;
    public decimal GrossWeight { get; set; }
    public string DeclaredValue { get; set; } = default!;
    public string Dimensions { get; set; } = default!;
    public decimal DimensionalWeight { get; set; }
    public int CountDimension { get; set; }
}
