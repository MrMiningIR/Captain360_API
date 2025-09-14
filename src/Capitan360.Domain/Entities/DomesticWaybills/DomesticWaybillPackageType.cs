using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.Companies;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Capitan360.Domain.Entities.DomesticWaybills;

public class DomesticWaybillPackageType : BaseEntity
{
    [ForeignKey(nameof(DomesticWaybill))]
    public int DomesticWaybillId { get; set; }
    public Company? DomesticWaybill { get; set; }

    [ForeignKey(nameof(CompanyPackageType))]
    public int CompanyPackageTypeId { get; set; }
    public CompanyPackageType? CompanyPackageType { get; set; }

    [ForeignKey(nameof(CompanyContentType))]
    public int CompanyContentTypeId { get; set; }
    public CompanyContentType? CompanyContentType { get; set; }
    public string? UserInsertedContentName { get; set; }

    public decimal GrossWeight { get; set; }

    public decimal ChargeableWeight { get; set; }

    public string DeclaredValue { get; set; } = default!;

    public string? Dimensions { get; set; }

    public int CountDimension { get; set; }
}
