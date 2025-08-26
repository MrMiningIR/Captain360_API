using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Entities.CompanyPackageEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.PackageEntity
{
    public class PackageType : Entity
    {
        [ForeignKey(nameof(CompanyType))]
        public int CompanyTypeId { get; set; }
        public CompanyType? CompanyType { get; set; }

        public string PackageTypeName { get; set; } = default!;

        public bool PackageTypeActive { get; set; }

        public string? PackageTypeDescription { get; set; }

        public int PackageTypeOrder { get; set; }

        public ICollection<CompanyPackageType> CompanyPackageTypes { get; set; } = [];
    }
}
