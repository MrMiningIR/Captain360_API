using Capitan360.Domain.Enums;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;

public class CompanyInsuranceChargePaymentContentTypeDto
{

    public int Id { get; set; }

    public int CompanyInsuranceChargeId { get; set; }

    public Rate Rate { get; set; }

    public int ContentId { get; set; }

    public decimal RateSettlement { get; set; }

    public bool IsPercentRateSettlement { get; set; }
    public decimal RateDiff { get; set; }
    public bool IsPercentDiff { get; set; }


}