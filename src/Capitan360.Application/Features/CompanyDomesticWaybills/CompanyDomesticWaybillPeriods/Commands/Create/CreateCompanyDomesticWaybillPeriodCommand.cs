namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Create;

public record CreateCompanyDomesticWaybillPeriodCommand(
    int CompanyId,
    string Code,
    long StartNumber,
    long EndNumber,
    bool Active,
    string Description);
