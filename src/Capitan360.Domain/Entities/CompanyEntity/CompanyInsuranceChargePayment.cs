using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyInsuranceChargePayment : Entity
{

    [ForeignKey(nameof(CompanyInsuranceCharge))]
    public int CompanyInsuranceChargeId { get; set; }
    public CompanyInsuranceCharge? CompanyInsuranceCharge { get; set; }
    public Rate Rate { get; set; }

    public decimal Diff { get; set; }

    public bool IsPercent { get; set; }

}