namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPeriods.Commands.Update;

public record UpdateDomesticWaybillPeriodCommand(
    string Code,
    long StartNumber,
    long EndNumber,
    bool Active,
    string Description)
{
    public int Id { get; set; }
};
