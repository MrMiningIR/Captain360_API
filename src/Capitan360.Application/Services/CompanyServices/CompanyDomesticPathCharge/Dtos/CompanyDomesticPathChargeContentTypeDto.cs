using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Constants;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Dtos;

public class CompanyDomesticPathChargeContentTypeDto
{
    public int Id { get; set; }
    [ForeignKey(nameof(CompanyDomesticPathCharge))]
    public int CompanyDomesticPathChargeId { get; set; }

    public WeightType WeightType { get; set; }
    public long Price { get; set; }

    public int ContentTypeId { get; set; }



    public int CompanyDomesticPathId { get; set; }




}