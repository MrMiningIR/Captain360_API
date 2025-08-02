using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.PackageEntity;

public class CompanyPackageType : Entity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }

    [ForeignKey(nameof(PackageType))]
    public int PackageTypeId { get; set; }

    public string PackageTypeName { get; set; } = default!;
    public bool Active { get; set; }

    public int OrderPackageType { get; set; }
    public string? PackageTypeDescription { get; set; }
    public Company Company { get; set; } = default!;
    public PackageType PackageType { get; set; } = default!;
}