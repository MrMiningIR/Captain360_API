namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Commands.Update;

public record UpdateCompanyDomesticPathReceiverCompanyCommand(
    int? ReceiverCompanyId,
    string? ReceiverCompanyUserInsertedCode,
    string? ReceiverCompanyUserInsertedName,
    string? ReceiverCompanyUserInsertedTelephone,
    string? ReceiverCompanyUserInsertedAddress,
    string DescriptionForPrint1,
    string DescriptionForPrint2,
    string DescriptionForPrint3)
{
    public int Id { get; set; }
}