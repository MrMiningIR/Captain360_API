namespace Capitan360.Application.Services.CompanyServices.CompanyPreferences.Dtos;

public class CompanyPreferencesDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; } 
    public string? EconomicCode { get; set; } = default!;
    public string? NationalId { get; set; } = default!;
    public string? RegistrationId { get; set; } = default!;
    public string? CaptainCargoName { get; set; } = default!;
    public string? CaptainCargoCode { get; set; } = default!;
    public bool ActiveIssueDomesticWaybill { get; set; }
    public bool ActiveShowInSearchEngine { get; set; }
    public bool ActiveInWebServiceSearchEngine { get; set; }
    public bool ActiveInternationalAirlineCargo { get; set; }
    public bool ExitStampBillMinWeightIsFixed { get; set; }
    public bool ExitPackagingMinWeightIsFixed { get; set; }
    public bool ExitAccumulationMinWeightIsFixed { get; set; }
    public bool ExitExtraSourceMinWeightIsFixed { get; set; }
    public bool ExitPricingMinWeightIsFixed { get; set; }
    public bool ExitRevenue1MinWeightIsFixed { get; set; }
    public bool ExitRevenue2MinWeightIsFixed { get; set; }
    public bool ExitRevenue3MinWeightIsFixed { get; set; }
    public decimal Tax { get; set; }
    public bool ExitFareInTax { get; set; }
    public bool ExitStampBillInTax { get; set; }
    public bool ExitPackagingInTax { get; set; }
    public bool ExitAccumulationInTax { get; set; }
    public bool ExitExtraSourceInTax { get; set; }
    public bool ExitPricingInTax { get; set; }
    public bool ExitRevenue1InTax { get; set; }
    public bool ExitRevenue2InTax { get; set; }
    public bool ExitRevenue3InTax { get; set; }
    public bool ExitDistributionInTax { get; set; }
    public bool ExitExtraDestinationInTax { get; set; }

}