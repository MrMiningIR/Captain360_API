using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.ContentTypes;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyInsuranceChargePaymentContentType : BaseEntity
{





    [ForeignKey(nameof(CompanyInsuranceCharge))]
    public int CompanyInsuranceChargeId { get; set; }
    public CompanyInsuranceCharge? CompanyInsuranceCharge { get; set; }


    public Rate Rate { get; set; }


    [ForeignKey(nameof(ContentType))]
    public int ContentId { get; set; }
    public ContentType? ContentType { get; set; }

     public decimal RateSettlement { get; set; }

     public bool IsPercentRateSettlement { get; set; }
     public decimal RateDiff { get; set; }
     public bool IsPercentDiff { get; set; }








}