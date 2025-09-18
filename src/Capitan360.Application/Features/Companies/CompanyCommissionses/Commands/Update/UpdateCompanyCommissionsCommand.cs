namespace Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Update;

public record UpdateCompanyCommissionsCommand(
    decimal CommissionFromCaptainCargoWebSite,
    decimal CommissionFromCompanyWebSite,
    decimal CommissionFromCaptainCargoWebService,
    decimal CommissionFromCompanyWebService,
    decimal CommissionFromCaptainCargoPanel,
    decimal CommissionFromCaptainCargoDesktop)
{
    public int Id { get; set; } 
}