namespace Capitan360.Application.Features.Companies.CompanyCommissionses.Commands.Create;

public record CreateCompanyCommissionsCommand(
    int CompanyId,
    long CommissionFromCaptainCargoWebSite,
    long CommissionFromCompanyWebSite,
    long CommissionFromCaptainCargoWebService,
    long CommissionFromCompanyWebService,
    long CommissionFromCaptainCargoPanel,
    long CommissionFromCaptainCargoDesktop);