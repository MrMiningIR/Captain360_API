namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.UpdateCompanyCommissions;

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