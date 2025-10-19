namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Commands.ChangeStateToBackToSenderCompanyFromDesktop;

public record ChangeStateDomesticWaybillToBackToSenderCompanyFromDesktopCommand(
    long No,
    string CompanyReceiverCaptain360Code,
    string DateUpdate,
    string TimeUpdate);
