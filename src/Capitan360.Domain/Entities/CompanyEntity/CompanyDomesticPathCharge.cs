using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyDomesticPathCharge : Entity
{
    [ForeignKey(nameof(CompanyDomesticPaths))]
    public int CompanyDomesticPathId { get; set; }
    public CompanyDomesticPaths? CompanyDomesticPaths { get; set; }
    public int ContentTypeId { get; set; }

    public WeightType WeightType { get; set; }
    public int Weight { get; set; }
    public long PriceDirect { get; set; }

    public bool ContentTypeChargeBaseNormal { get; set; }

    public ICollection<CompanyDomesticPathChargeContentType> CompanyDomesticPathChargeContentTypes { get; set; } = [];



}