namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Commands.Issue;

public record IssueCompanyDomesticWaybillPackageTypeCommand(
     int CompanyDomesticWaybillId,
     int CompanyPackageTypeId,
     int CompanyContentTypeId,
     string UserInsertedContentName,
     decimal GrossWeight,
     string DeclaredValue,
     string Dimensions,
     decimal DimensionalWeight,
     int CountDimension);
