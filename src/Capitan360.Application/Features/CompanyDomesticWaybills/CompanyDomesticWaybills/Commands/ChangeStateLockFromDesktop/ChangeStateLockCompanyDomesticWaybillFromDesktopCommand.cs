namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateLockFromDesktop;

public record ChangeStateLockCompanyDomesticWaybillFromDesktopCommand(
    long No,
    string CompanySenderCaptain360Code,
    bool Lock,
    string DateUpdate,
    string TimeUpdate);
