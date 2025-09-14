using Capitan360.Domain.Enums;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;

public class CompanyInsuranceChargePaymentDto 
{


    public int Id { get; set; }
    public int CompanyInsuranceChargeId { get; set; }
    public Rate Rate { get; set; }

    public decimal Diff { get; set; }

    public bool IsPercent { get; set; }

}