namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Commands.AssignMasterWaybill;

public record AssignMasterWaybillCommand(
string CompanySenderCaptain360Code,
string? MasterWaybillNo,
decimal? MasterWaybillWeight,
string? MasterWaybillAirline,
string? MasterWaybillFlightNo,
string? MasterWaybillFlightDate)
{
    public int Id { get; set; }
}
