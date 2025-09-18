using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyDomesticPath : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    [ForeignKey(nameof(SourceCountry))]
    public int SourceCountryId { get; set; }
    public Area? SourceCountry { get; set; }

    [ForeignKey(nameof(SourceProvince))]
    public int SourceProvinceId { get; set; }
    public Area? SourceProvince { get; set; } 

    [ForeignKey(nameof(SourceCity))]
    public int SourceCityId { get; set; }
    public Area? SourceCity { get; set; } 

    [ForeignKey(nameof(DestinationCountry))]
    public int DestinationCountryId { get; set; }
    public Area? DestinationCountry { get; set; } 

    [ForeignKey(nameof(DestinationProvince))]
    public int DestinationProvinceId { get; set; }
    public Area? DestinationProvince { get; set; } 

    [ForeignKey(nameof(DestinationCity))]
    public int DestinationCityId { get; set; }
    public Area? DestinationCity { get; set; } 

    public bool Active { get; set; }

    public string Description { get; set; } = default!;

    public string DescriptionForSearch { get; set; } = default!;

    public long EntranceFee { get; set; }

    public decimal EntranceFeeWeight { get; set; }

    public int EntranceFeeType { get; set; }

    public bool ExitStampBillMinWeightIsFixed { get; set; }

    public bool ExitPackagingMinWeightIsFixed { get; set; }

    public bool ExitAccumulationMinWeightIsFixed { get; set; }

    public bool ExitExtraSourceMinWeightIsFixed { get; set; }

    public bool ExitPricingMinWeightIsFixed { get; set; }

    public bool ExitRevenue1MinWeightIsFixed { get; set; }

    public bool ExitRevenue2MinWeightIsFixed { get; set; }

    public bool ExitRevenue3MinWeightIsFixed { get; set; }

    public bool ExitDistributionMinWeightIsFixed { get; set; }

    public bool ExitExtraDestinationMinWeightIsFixed { get; set; }

    public ICollection<CompanyDomesticPathStructPrice> CompanyDomesticPathStructPrices { get; set; } = [];

    public ICollection<CompanyDomesticPathCharge> CompanyDomesticPathCharges { get; set; } = [];

    public ICollection<CompanyDomesticPathStructPriceMunicipalArea> CompanyDomesticPathStructPriceMunicipalAreas { get; set; } = [];

    public ICollection<CompanyDomesticPathChargeContentType> CompanyDomesticPathChargeContentTypes { get; set; } = [];
}