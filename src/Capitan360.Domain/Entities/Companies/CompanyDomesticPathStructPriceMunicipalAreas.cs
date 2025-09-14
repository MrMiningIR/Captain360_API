using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyDomesticPathStructPriceMunicipalAreas : BaseEntity
{
    [ForeignKey(nameof(CompanyDomesticPaths))]
    public int CompanyDomesticPathId { get; set; }

    public CompanyDomesticPaths? CompanyDomesticPaths { get; set; }

    [ForeignKey(nameof(CompanyDomesticPathStructPrice))]
    public int CompanyDomesticPathStructPriceId { get; set; }
    public CompanyDomesticPathStructPrices? CompanyDomesticPathStructPrice { get; set; }

    [ForeignKey(nameof(Area))]
    public int MunicipalAreaId { get; set; }
    public Area? Area { get; set; }

    public WeightType WeightType { get; set; }
    public PathStructType PathStructType { get; set; }
    public long Price { get; set; }
    public bool Static { get; set; }

}