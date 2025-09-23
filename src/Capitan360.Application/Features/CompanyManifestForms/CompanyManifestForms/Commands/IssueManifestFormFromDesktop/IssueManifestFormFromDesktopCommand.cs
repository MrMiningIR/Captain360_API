namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.IssueManifestFormFromDesktop;

public record IssueManifestFormFromDesktopCommand(
long No,
string CompanySenderCaptain360Code,
string CompanyReceiverCaptain360Code,
string CompanyReceiverCaptain360Name,
string Date,
string? CompanySenderDescription,
string? CompanySenderDescriptionForPrint,
string DateIssued,
string TimeIssued
);
