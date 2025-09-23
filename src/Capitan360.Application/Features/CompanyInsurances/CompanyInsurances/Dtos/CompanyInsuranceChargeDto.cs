using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyInsurances.CompanyInsurances.Dtos;

public class CompanyInsuranceChargeDto
{
    public int Id { get; set; }
    public Rate Rate { get; set; }
    public decimal Value { get; set; }
    public decimal Settlement { get; set; }
    public bool IsPercent { get; set; }
    public bool Static { get; set; }
    public int CompanyInsuranceId { get; set; }

    public ICollection<CompanyInsuranceChargePaymentContentTypeDto> CompanyInsuranceChargePaymentContentTypesDto { get; set; } = [];


    public ICollection<CompanyInsuranceChargePaymentDto> CompanyInsuranceChargePaymentsDto { get; set; } = [];
}