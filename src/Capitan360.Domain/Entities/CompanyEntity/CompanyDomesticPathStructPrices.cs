using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyDomesticPathStructPrices : Entity
{
    [ForeignKey(nameof(CompanyDomesticPaths))]
    public int CompanyDomesticPathId { get; set; }
    public CompanyDomesticPaths? CompanyDomesticPaths { get; set; }
    public int Weight { get; set; }
    public PathStructType PathStructType { get; set; }
    public WeightType WeightType { get; set; }

    public int MunicipalAreaId { get; set; }

    public ICollection<CompanyDomesticPathStructPriceMunicipalAreas> CompanyDomesticPathStructPriceMunicipalAreas { get; set; } = [];

}