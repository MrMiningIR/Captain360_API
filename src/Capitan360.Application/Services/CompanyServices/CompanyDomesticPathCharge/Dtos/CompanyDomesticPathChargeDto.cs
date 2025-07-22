using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Dtos;

public class CompanyDomesticPathChargeDto
{
    public int Id { get; set; }
    public int CompanyDomesticPathId { get; set; }
    public WeightType WeightType { get; set; }
    public int Weight { get; set; }
    public long Price { get; set; }
    public bool ContentTypeChargeBaseNormal { get; set; }

    public List<CompanyDomesticPathChargeContentTypeDto> CompanyDomesticPathChargeContentTypesDtos { get; set; } = [];


}