using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.CompanyDomesticPaths;

public class CompanyDomesticPathCharge : BaseEntity
{
    [ForeignKey(nameof(CompanyDomesticPaths))]
    public int CompanyDomesticPathId { get; set; }
    public CompanyDomesticPath? CompanyDomesticPaths { get; set; }
    public int ContentTypeId { get; set; }

    public WeightType WeightType { get; set; }
    public int Weight { get; set; }
    public long PriceDirect { get; set; }

    public bool ContentTypeChargeBaseNormal { get; set; }

    public ICollection<CompanyDomesticPathChargeContentType> CompanyDomesticPathChargeContentTypes { get; set; } = [];



}