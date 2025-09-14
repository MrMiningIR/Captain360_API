using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyDomesticPathStructPrices : BaseEntity
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