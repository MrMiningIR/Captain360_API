namespace Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Update;

public record UpdateCompanyCommissionsCommand(
    long CommissionFromCaptainCargoWebSite,
    long CommissionFromCompanyWebSite,
    long CommissionFromCaptainCargoWebService,
    long CommissionFromCompanyWebService,
    long CommissionFromCaptainCargoPanel,
    long CommissionFromCaptainCargoDesktop)
{
    public int Id { get; set; } 
}