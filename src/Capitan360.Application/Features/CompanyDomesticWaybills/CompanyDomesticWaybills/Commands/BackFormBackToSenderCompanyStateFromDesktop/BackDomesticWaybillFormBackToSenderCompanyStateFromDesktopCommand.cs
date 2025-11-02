namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.BackFormBackToSenderCompanyStateFromDesktop;

public record BackDomesticWaybillFormBackToSenderCompanyStateFromDesktopCommand(
    long No,
    string CompanyReceiverCaptain360Code,
    string DateUpdate,
    string TimeUpdate);
