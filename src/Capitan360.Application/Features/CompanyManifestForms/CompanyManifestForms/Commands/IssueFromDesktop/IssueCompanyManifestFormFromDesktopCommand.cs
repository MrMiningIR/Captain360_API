namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.IssueFromDesktop
{
    public record IssueCompanyManifestFormFromDesktopCommand(
        long No,
        string CompanySenderCaptain360Code,
        string CompanyReceiverUserInsertedCode,
        string Date,
        string CompanySenderDescription,
        string CompanySenderDescriptionForPrint,
        string DateIssued,
        string TimeIssued);
}