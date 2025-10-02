namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AttachToCompanyManifestFormFromDesktop;

public record AttachCompanyDomesticWaybillToCompanyManifestFormFromDesktopCommand(
   long No,
   string CompanySenderCaptain360Code,
   long CompanyManifestFormNo,
   string DateManifested,
   string TimeManifested);
