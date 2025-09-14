using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyPreferences : Entity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public string? EconomicCode { get; set; }

    public string? NationalId { get; set; }

    public string? RegistrationId { get; set; }

    public string? CaptainCargoName { get; set; }

    public string? CaptainCargoCode { get; set; }

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