namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Commands.UpdateCompanyCommissions;

public record UpdateCompanyCommissionsCommand(
    int Id,
    decimal? CommissionFromCaptainCargoWebSite,
    decimal? CommissionFromCompanyWebSite,
    decimal? CommissionFromCaptainCargoWebService,
    decimal? CommissionFromCompanyWebService,
    decimal? CommissionFromCaptainCargoPanel,
    decimal? CommissionFromCaptainCargoDesktop);