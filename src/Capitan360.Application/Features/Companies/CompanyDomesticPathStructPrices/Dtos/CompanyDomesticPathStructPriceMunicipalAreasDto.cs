using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Dtos;

public class CompanyDomesticPathStructPriceMunicipalAreasDto
{
    public int CompanyDomesticPathStructPriceId { get; set; }
    public int MunicipalAreaId { get; set; }
    public WeightType WeightType { get; set; }
    public PathStructType PathStructType { get; set; }
    public long Price { get; set; }
    public bool Static { get; set; }
    public int CompanyDomesticPathId { get; set; }
}