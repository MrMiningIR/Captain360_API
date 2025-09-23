using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Entities.CompanyDomesticPaths;

public class CompanyDomesticPathStructPrice : BaseEntity
{
    [ForeignKey(nameof(CompanyDomesticPaths))]
    public int CompanyDomesticPathId { get; set; }
    public CompanyDomesticPath? CompanyDomesticPaths { get; set; }
    public int Weight { get; set; }
    public PathStructType PathStructType { get; set; }
    public WeightType WeightType { get; set; }

    public int MunicipalAreaId { get; set; }

    public ICollection<CompanyDomesticPathStructPriceMunicipalArea> CompanyDomesticPathStructPriceMunicipalAreas { get; set; } = [];

}