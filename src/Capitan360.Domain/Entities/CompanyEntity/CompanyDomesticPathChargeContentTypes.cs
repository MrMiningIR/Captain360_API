using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.ContentEntity;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyDomesticPathChargeContentType:Entity
{

    [ForeignKey(nameof(CompanyDomesticPathCharge))]
    public int CompanyDomesticPathChargeId { get; set; }
    public CompanyDomesticPathCharge? CompanyDomesticPathCharge { get; set; }

    public WeightType WeightType { get; set; }
    public long Price { get; set; }


    [ForeignKey(nameof(ContentType))]
    public int ContentTypeId  { get; set; }

    public ContentType? ContentType { get; set; }
  
  

    [ForeignKey(nameof(CompanyDomesticPaths))]
    public int CompanyDomesticPathId { get; set; }
    public CompanyDomesticPaths? CompanyDomesticPaths { get; set; }




    
}