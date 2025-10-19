namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackToReadyStateFromDesktop;

public record BackCompanyDomesticWaybillToReadyStateFromDesktopCommand(
    long No,
    string CompanySenderCaptain360Code,
    string DateUpdate,
    string TimeUpdate);