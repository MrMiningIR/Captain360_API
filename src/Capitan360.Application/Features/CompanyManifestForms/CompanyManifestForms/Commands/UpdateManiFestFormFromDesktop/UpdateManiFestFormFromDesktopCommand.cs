namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.UpdateManiFestFormFromDesktop;

public record UpdateManiFestFormFromDesktopCommand(
    long No,
    string CompanySenderCaptain360Code,
    string? CompanySenderDescription,
    string? CompanySenderDescriptionForPrint
    );
