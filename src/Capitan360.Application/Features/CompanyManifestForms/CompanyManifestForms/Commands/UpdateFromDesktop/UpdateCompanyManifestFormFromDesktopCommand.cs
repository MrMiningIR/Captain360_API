namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.UpdateFromDesktop;

public record UpdateCompanyManifestFormFromDesktopCommand(
    long No,
    string CompanySenderCaptain360Code,
    string CompanySenderDescription,
    string CompanySenderDescriptionForPrint,
    string DateUpdate,
    string TimeUpdate);
