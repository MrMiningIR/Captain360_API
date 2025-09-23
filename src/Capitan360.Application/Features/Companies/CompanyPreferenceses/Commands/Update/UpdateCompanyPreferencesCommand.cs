namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Commands.Update;

public record UpdateCompanyPreferencesCommand(
    string EconomicCode,
    string NationalId,
    string RegistrationId,
    string CaptainCargoName,
    string CaptainCargoCode,
    bool ExitStampBillMinWeightIsFixed,
    bool ExitPackagingMinWeightIsFixed,
    bool ExitAccumulationMinWeightIsFixed,
    bool ExitExtraSourceMinWeightIsFixed,
    bool ExitPricingMinWeightIsFixed,
    bool ExitRevenue1MinWeightIsFixed,
    bool ExitRevenue2MinWeightIsFixed,
    bool ExitRevenue3MinWeightIsFixed,
    decimal Tax,
    bool ExitFareInTax,
    bool ExitStampBillInTax,
    bool ExitPackagingInTax,
    bool ExitAccumulationInTax,
    bool ExitExtraSourceInTax,
    bool ExitPricingInTax,
    bool ExitRevenue1InTax,
    bool ExitRevenue2InTax,
    bool ExitRevenue3InTax,
    bool ExitDistributionInTax,
    bool ExitExtraDestinationInTax)
{
    public int Id { get; set; }
}