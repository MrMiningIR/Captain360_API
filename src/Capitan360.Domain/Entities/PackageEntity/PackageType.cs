using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.CompanyEntity;

namespace Capitan360.Domain.Entities.PackageEntity
{
    public class PackageType : Entity
    {
        public int CompanyTypeId { get; set; }
        public CompanyType CompanyType { get; set; } = default!;

        public string PackageTypeName { get; set; } = default!;

        public bool Active { get; set; }

        public string PackageTypeDescription { get; set; } = default!;

        public int OrderPackageType { get; set; }

        public ICollection<CompanyPackageType> CompanyPackageTypes { get; set; } = [];
    }
}