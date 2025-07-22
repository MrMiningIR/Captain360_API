using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.AddressEntity;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyDomesticPathStructPriceMunicipalAreas:Entity
{
    [ForeignKey(nameof(CompanyDomesticPathStructPrice))]
    public int CompanyDomesticPathStructPriceId { get; set; }
    [ForeignKey(nameof(Area))]
    public int MunicipalAreaId { get; set; }
    public WeightType WeightType { get; set; }
    public PathStructType PathStructType { get; set; }
    public long Price { get; set; }
    public bool Static { get; set; }

    [ForeignKey(nameof(CompanyDomesticPaths))]
    public int CompanyDomesticPathId { get; set; }
    public CompanyDomesticPaths? CompanyDomesticPaths { get; set; }

    public CompanyDomesticPathStructPrices? CompanyDomesticPathStructPrice { get; set; }
    public Area? Area { get; set; }

    
}

