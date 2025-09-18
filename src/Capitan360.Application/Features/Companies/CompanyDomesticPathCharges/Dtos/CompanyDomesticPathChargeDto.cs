using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathCharges.Dtos;

public class CompanyDomesticPathChargeDto
{
    public int Id { get; set; }
    public int CompanyDomesticPathId { get; set; }
    public WeightType WeightType { get; set; }
    public int Weight { get; set; }
    public long PriceDirect { get; set; }
    public bool ContentTypeChargeBaseNormal { get; set; }

    public List<CompanyDomesticPathChargeContentTypeDto> CompanyDomesticPathChargeContentTypesDtos { get; set; } = [];


}