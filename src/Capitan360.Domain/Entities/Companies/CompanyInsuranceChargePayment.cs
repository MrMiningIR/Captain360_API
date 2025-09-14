using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyInsuranceChargePayment : BaseEntity
{

    [ForeignKey(nameof(CompanyInsuranceCharge))]
    public int CompanyInsuranceChargeId { get; set; }
    public CompanyInsuranceCharge? CompanyInsuranceCharge { get; set; }
    public Rate Rate { get; set; }

    public decimal Diff { get; set; }

    public bool IsPercent { get; set; }

}