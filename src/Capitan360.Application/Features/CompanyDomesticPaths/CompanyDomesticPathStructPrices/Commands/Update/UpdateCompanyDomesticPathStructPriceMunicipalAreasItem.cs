using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Update;

public record UpdateCompanyDomesticPathStructPriceMunicipalAreasItem
{

    public int Id { get; set; }
    public int CompanyDomesticPathStructPriceId { get; set; }
    public int MunicipalAreaId { get; set; }
    public WeightType WeightType { get; set; }
    public long Price { get; set; }
    public bool Static { get; set; }
    public int CompanyDomesticPathId { get; set; }
    public PathStructType PathStructType { get; set; }


};