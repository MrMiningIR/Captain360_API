using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Dtos;

public class CompanyDomesticWaybillPackageTypeDto
{
    public int Id { get; set; }
    public int DomesticWaybillId { get; set; }
    public string? DomesticWaybillNo { get; set; }
    public int CompanyPackageTypeId { get; set; }
    public string? CompanyPackageTypeName { get; set; }
    public int CompanyContentTypeId { get; set; }
    public string? CompanyContentTypeName { get; set; }
    public string? UserInsertedContentName { get; set; }
    public decimal GrossWeight { get; set; }
    public string DeclaredValue { get; set; } = default!;
    public string? Dimensions { get; set; }
    public decimal? DimensionalWeight { get; set; }
    public int CountDimension { get; set; }
}
