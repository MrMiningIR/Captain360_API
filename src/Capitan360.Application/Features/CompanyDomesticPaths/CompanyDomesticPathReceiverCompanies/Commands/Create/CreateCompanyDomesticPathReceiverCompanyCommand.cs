namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Create;

public record CreateCompanyDomesticPathReceiverCompanyCommand(
    int CompanyDomesticPathId,
    int MunicipalAreaId,
    int? ReceiverCompanyId,
    string? ReceiverCompanyUserInsertedCode,
    string? ReceiverCompanyUserInsertedName,
    string? ReceiverCompanyUserInsertedTelephone,
    string? ReceiverCompanyUserInsertedAddress,
    string DescriptionForPrint1,
    string DescriptionForPrint2,
    string DescriptionForPrint3);