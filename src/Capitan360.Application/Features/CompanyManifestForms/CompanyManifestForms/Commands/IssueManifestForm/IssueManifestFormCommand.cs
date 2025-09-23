namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.IssueManifestForm;

public record IssueManifestFormCommand(
long No,
int CompanySenderId,
int? CompanyReceiverId,
string? CompanyReceiverCaptain360Code,
string? CompanyReceiverCaptain360Name,
string Date,
string? CompanySenderDescription,
string? CompanySenderDescriptionForPrint
);