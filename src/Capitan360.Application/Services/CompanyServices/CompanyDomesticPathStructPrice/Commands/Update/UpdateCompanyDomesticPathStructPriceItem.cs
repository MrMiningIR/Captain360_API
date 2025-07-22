using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Update;

public record UpdateCompanyDomesticPathStructPriceItem
{
    public int CompanyDomesticPathId { get; set; }
    public WeightType WeightType { get; set; }
    public int MunicipalAreaId { get; set; } = 0;
    public int Weight { get; set; }
    public int? Id { get; set; }
    public PathStructType PathStructType { get; set; }
    public UpdateCompanyDomesticPathStructPriceMunicipalAreasCommand? StructPriceArea { get; set; } = new();

}