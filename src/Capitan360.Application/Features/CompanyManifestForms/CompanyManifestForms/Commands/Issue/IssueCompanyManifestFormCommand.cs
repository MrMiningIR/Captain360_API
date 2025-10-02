namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.Issue;

public record IssueCompanyManifestFormCommand(
    long No,
    int? CompanyReceiverId,
    string? CompanyReceiverUserInsertedCode,
    string Date,
    string CompanySenderDescription,
    string CompanySenderDescriptionForPrint);