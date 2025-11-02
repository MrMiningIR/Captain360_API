namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.IssueFromDesktop;

public record IssueCompanyDomesticWaybillPackageTypeFromDesktopCommand(
    int CompanyDomesticWaybillId,
    string UserInsertedPackageName,
    string UserInsertedContentName,
    decimal GrossWeight,
    string DeclaredValue,
    string Dimensions,
    decimal DimensionalWeight,
    int CountDimension);