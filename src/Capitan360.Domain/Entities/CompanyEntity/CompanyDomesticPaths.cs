using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.AddressEntity;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyDomesticPaths : Entity
{

    public int Active { get; set; }

    public string Description { get; set; } = default!;
    public string DescriptionForSearch { get; set; } = default!;

    public long EntranceFee { get; set; }
    public decimal EntranceWeight { get; set; }

    public int EntranceType { get; set; }

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
    


    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    [ForeignKey(nameof(SourceCountry))]
    public int SourceCountryId { get; set; }
    public Area SourceCountry { get; set; } = null!;

    [ForeignKey(nameof(SourceProvince))]
    public int SourceProvinceId { get; set; }
    public Area SourceProvince { get; set; } = null!;

    [ForeignKey(nameof(SourceCity))]
    public int SourceCityId { get; set; }
    public Area SourceCity { get; set; } = null!;

    [ForeignKey(nameof(DestinationCountry))]
    public int DestinationCountryId { get; set; }
    public Area DestinationCountry { get; set; } = null!;

    [ForeignKey(nameof(DestinationProvince))]
    public int DestinationProvinceId { get; set; }
    public Area DestinationProvince { get; set; } = null!;

    [ForeignKey(nameof(DestinationCity))]
    public int DestinationCityId { get; set; }
    public Area DestinationCity { get; set; } = null!;


    public ICollection<CompanyDomesticPathStructPrices> CompanyDomesticPathStructPrices { get; set; } = [];
    public ICollection<CompanyDomesticPathCharge> CompanyDomesticPathCharges { get; set; } = [];
    public ICollection<CompanyDomesticPathStructPriceMunicipalAreas> CompanyDomesticPathStructPriceMunicipalAreas { get; set; } = [];
    public ICollection<CompanyDomesticPathChargeContentType> CompanyDomesticPathChargeContentTypes { get; set; } = [];

}