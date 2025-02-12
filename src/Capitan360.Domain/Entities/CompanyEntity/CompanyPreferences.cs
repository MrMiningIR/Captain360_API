using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyPreferences:Entity
{

      
    public bool Admin { get; set; }
    public short Type { get; set; }
    public bool ActiveIssueDomesticWaybill { get; set; }
    public bool ActiveShowInSearchEngine { get; set; }
    public bool ActiveInWebServiceSearchEngine { get; set; }
    public bool ActiveInternationalAirlineCargo { get; set; }

    // Weight-related settings
    public bool ExitStampBillMinWeightIsFixed { get; set; }
    public bool ExitPackagingMinWeightIsFixed { get; set; }
    public bool ExitAccumulationMinWeightIsFixed { get; set; }
    public bool ExitExtraSourceMinWeightIsFixed { get; set; }
    public bool ExitPricingMinWeightIsFixed { get; set; }
    public bool ExitRevenue1MinWeightIsFixed { get; set; }
    public bool ExitRevenue2MinWeightIsFixed { get; set; }
    public bool ExitRevenue3MinWeightIsFixed { get; set; }

    // Tax-related settings
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

    
    // Navigation Properties

    public int CompanyId { get; set; }
    public  Company Company { get; set; } = null!;
}