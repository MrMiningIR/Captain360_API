using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Dtos;

public class CompanyDomesticPathStructPriceDto
{
    public int Id { get; set; }
    public int CompanyDomesticPathId { get; set; }
    public int Weight { get; set; }
    public PathStructType PathStructType { get; set; }
    public WeightType WeightType { get; set; }
    public ICollection<CompanyDomesticPathStructPriceMunicipalAreasDto> CompanyDomesticPathStructPriceMunicipalAreasDtos { get; set; } = [];
}