using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Entities.CompanyInsurances;

public class CompanyInsuranceChargePayment : BaseEntity
{

    [ForeignKey(nameof(CompanyInsuranceCharge))]
    public int CompanyInsuranceChargeId { get; set; }
    public CompanyInsuranceCharge? CompanyInsuranceCharge { get; set; }
    public Rate Rate { get; set; }

    public decimal Diff { get; set; }

    public bool IsPercent { get; set; }

}