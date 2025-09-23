using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.ContentTypes;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Entities.CompanyDomesticPaths;

public class CompanyDomesticPathChargeContentType : BaseEntity
{

    [ForeignKey(nameof(CompanyDomesticPathCharge))]
    public int CompanyDomesticPathChargeId { get; set; }
    public CompanyDomesticPathCharge? CompanyDomesticPathCharge { get; set; }

    public WeightType WeightType { get; set; }
    public long Price { get; set; }


    [ForeignKey(nameof(ContentType))]
    public int ContentTypeId { get; set; }

    public ContentType? ContentType { get; set; }



    [ForeignKey(nameof(CompanyDomesticPaths))]
    public int CompanyDomesticPathId { get; set; }
    public CompanyDomesticPath? CompanyDomesticPaths { get; set; }





}