namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybillPackageTypes.Dtos;

public class CompanyDomesticWaybillPackageTypeDto
{
    public int Id { get; set; }
    public int CompanyDomesticWaybillId { get; set; }
    public string? CompanyDomesticWaybillNo { get; set; }
    public int CompanyPackageTypeId { get; set; }
    public string? CompanyPackageTypeName { get; set; }
    public int CompanyContentTypeId { get; set; }
    public string? CompanyContentTypeName { get; set; }
    public string UserInsertedContentName { get; set; } = default!;
    public decimal GrossWeight { get; set; }
    public string DeclaredValue { get; set; } = default!;
    public string Dimensions { get; set; } = default!;
    public decimal DimensionalWeight { get; set; }
    public int CountDimension { get; set; }
}
