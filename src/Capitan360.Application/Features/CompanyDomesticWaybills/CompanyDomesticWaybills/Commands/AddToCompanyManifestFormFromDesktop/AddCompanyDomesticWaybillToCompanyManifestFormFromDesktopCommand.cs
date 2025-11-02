namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.AddToCompanyManifestFormFromDesktop;

public record AddCompanyDomesticWaybillToCompanyManifestFormFromDesktopCommand(
   long No,
   string CompanySenderCaptain360Code,
   long CompanyManifestFormNo,
   string DateManifested,
   string TimeManifested);
