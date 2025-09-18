namespace Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Create;

public record CreateCompanyCommissionsCommand(
    int CompanyId,
    decimal CommissionFromCaptainCargoWebSite,
    decimal CommissionFromCompanyWebSite,
    decimal CommissionFromCaptainCargoWebService,
    decimal CommissionFromCompanyWebService,
    decimal CommissionFromCaptainCargoPanel,
    decimal CommissionFromCaptainCargoDesktop);