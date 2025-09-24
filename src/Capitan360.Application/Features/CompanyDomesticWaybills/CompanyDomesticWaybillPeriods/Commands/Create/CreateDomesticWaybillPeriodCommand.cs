namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Create;

public record CreateDomesticWaybillPeriodCommand(
    int CompanyId,
    string Code,
    long StartNumber,
    long EndNumber,
    bool Active,
    string Description);
