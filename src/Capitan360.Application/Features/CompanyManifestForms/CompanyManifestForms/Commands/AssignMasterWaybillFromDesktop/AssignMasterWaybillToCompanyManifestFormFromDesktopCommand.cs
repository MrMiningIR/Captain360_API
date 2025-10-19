namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AssignMasterWaybillFromDesktop;

public record AssignMasterWaybillToCompanyManifestFormFromDesktopCommand(
    long No,
    string CompanySenderCaptain360Code,
    string MasterWaybillNo,
    decimal MasterWaybillWeight,
    string MasterWaybillAirline,
    string MasterWaybillFlightNo,
    string MasterWaybillFlightDate,
    string DateUpdate,
    string TimeUpdate);
