using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Enums;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyInsuranceCharge : BaseEntity
{

    public Rate Rate { get; set; }

    public decimal Value { get; set; }

    public decimal Settlement { get; set; }

    public bool IsPercent { get; set; }

    public bool Static { get; set; }


    [ForeignKey(nameof(CompanyInsurance))]
    public int CompanyInsuranceId { get; set; }
    public CompanyInsurance?  CompanyInsurance { get; set; }


    public ICollection<CompanyInsuranceChargePaymentContentType> CompanyInsuranceChargePaymentContentTypes { get; set; } = [];


    public ICollection<CompanyInsuranceChargePayment> CompanyInsuranceChargePayments { get; set; } = [];


}

