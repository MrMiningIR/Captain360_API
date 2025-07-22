using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPath.Dtos;

public class CompanyDomesticPathDto
{
    public int Id { get; set; }
    public int Active { get; set; }
    public string Description { get; set; } = default!;
    public string DescriptionForSearch { get; set; } = default!;
    public long EntranceFee { get; set; }
    public decimal EntranceWeight { get; set; }
    public int EntranceType { get; set; }
    public int CompanyId { get; set; }
    public int SourceCountryId { get; set; }
    public string SourceCountryName { get; set; } = default!;
    public int SourceProvinceId { get; set; }
    public string SourceProvinceName { get; set; } = default!;
    public int SourceCityId { get; set; }
    public string SourceCityName { get; set; } = default!;
    public int DestinationCountryId { get; set; }
    public string DestinationCountryName { get; set; } = default!;
    public int DestinationProvinceId { get; set; }
    public string DestinationProvinceName { get; set; } = default!;
    public int DestinationCityId { get; set; }
    public string DestinationCityName { get; set; } = default!;
}