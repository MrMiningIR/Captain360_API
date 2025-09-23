namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToManifestFormFromDesktop;

public record AttachToCompanyManifestFormFromDesktopCommand(
   long No,
   string CompanySenderCaptain360Code,
   long CompanyManifestFormNo,
   string DateManifested,
   string TimeManifested
   );
