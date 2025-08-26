using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Entities.PackageEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.CompanyPackageEntity;

public class CompanyPackageType : Entity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }

    public Company? Company { get; set; }

    [ForeignKey(nameof(PackageType))]
    public int PackageTypeId { get; set; }

    public PackageType? PackageType { get; set; }

    public string CompanyPackageTypeName { get; set; } = default!;
    public bool CompanyPackageTypeActive { get; set; }

    public int CompanyPackageTypeOrder { get; set; }
    public string? CompanyPackageTypeDescription { get; set; }
}