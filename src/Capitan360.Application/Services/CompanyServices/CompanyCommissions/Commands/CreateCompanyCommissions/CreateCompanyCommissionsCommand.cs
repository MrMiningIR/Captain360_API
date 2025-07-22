namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.CreateCompanyCommissions;

public record CreateCompanyCommissionsCommand(
    int CompanyId,
    decimal CommissionFromCaptainCargoWebSite,
    decimal CommissionFromCompanyWebSite,
    decimal CommissionFromCaptainCargoWebService,
    decimal CommissionFromCompanyWebService,
    decimal CommissionFromCaptainCargoPanel,
    decimal CommissionFromCaptainCargoDesktop);