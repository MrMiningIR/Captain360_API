using Capitan360.Domain.Enums;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Dtos;

public class CompanyDomesticPathChargeContentTypeDto
{
    public int Id { get; set; }
    public int CompanyDomesticPathChargeId { get; set; }

    public WeightType WeightType { get; set; }
    public long Price { get; set; }

    //IsPercent

    public int ContentTypeId { get; set; }



    public int CompanyDomesticPathId { get; set; }




}