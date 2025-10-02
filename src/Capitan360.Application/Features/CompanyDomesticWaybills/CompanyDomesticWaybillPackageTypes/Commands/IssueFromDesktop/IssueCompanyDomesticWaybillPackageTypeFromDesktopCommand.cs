namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.IssueFromDesktop;

public class IssueCompanyDomesticWaybillPackageTypeFromDesktopCommand
{
    public int CompanyDomesticWaybillId { get; set; }
    public string UserInsertedPackageName { get; set; } = default!;
    public string UserInsertedContentName { get; set; } = default!;
    public decimal GrossWeight { get; set; }
    public string DeclaredValue { get; set; } = default!;
    public string Dimensions { get; set; } = default!;
    public decimal DimensionalWeight { get; set; }
    public int CountDimension { get; set; }
}
