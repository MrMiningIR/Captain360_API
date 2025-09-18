namespace Capitan360.Application.Features.Companies.CompanyDomesticPaths.Dtos;

public class CompanyDomesticPathDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; } 
    public int SourceCountryId { get; set; }
    public string? SourceCountryName { get; set; } 
    public int SourceProvinceId { get; set; }
    public string? SourceProvinceName { get; set; } 
    public int SourceCityId { get; set; }
    public string? SourceCityName { get; set; } 
    public int DestinationCountryId { get; set; }
    public string? DestinationCountryName { get; set; } 
    public int DestinationProvinceId { get; set; }
    public string? DestinationProvinceName { get; set; } 
    public int DestinationCityId { get; set; }
    public string? DestinationCityName { get; set; } 
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
}