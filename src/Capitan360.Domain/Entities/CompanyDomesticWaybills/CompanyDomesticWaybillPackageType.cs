using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.Companies;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Capitan360.Domain.Entities.CompanyDomesticWaybills;

public class CompanyDomesticWaybillPackageType : BaseEntity
{
    [ForeignKey(nameof(CompanyDomesticWaybill))]
    public int CompanyDomesticWaybillId { get; set; }
    public CompanyDomesticWaybill? CompanyDomesticWaybill { get; set; }

    [ForeignKey(nameof(CompanyPackageType))]
    public int CompanyPackageTypeId { get; set; }
    public CompanyPackageType? CompanyPackageType { get; set; }

    [ForeignKey(nameof(CompanyContentType))]
    public int CompanyContentTypeId { get; set; }
    public CompanyContentType? CompanyContentType { get; set; }
    public string? UserInsertedContentName { get; set; }

    public decimal GrossWeight { get; set; }

    public string DeclaredValue { get; set; } = default!;

    public string Dimensions { get; set; } = default!;

    public decimal DimensionalWeight { get; set; }

    public int CountDimension { get; set; }
}
