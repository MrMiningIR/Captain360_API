using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.AddressEntity;
using Capitan360.Domain.Entities.ContentEntity;
using Capitan360.Domain.Entities.PackageEntity;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class Company : Entity
{
    public string Code { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int CompanyTypeId { get; set; }
    public bool IsParentCompany { get; set; }
    public bool Active { get; set; }
    public int CountryId { get; set; }

    public int ProvinceId { get; set; }

    public int CityId { get; set; }
    public string? Description { get; set; }

    // Navigation Properties

    public ICollection<UserCompany> UserCompanies { get; set; } = [];

    public ICollection<CompanyUri> CompanyUris { get; set; } = [];
    public ICollection<CompanyContentType> CompanyContentTypes { get; set; } = [];
    public ICollection<CompanyPackageType> CompanyPackageTypes { get; set; } = [];

    public ICollection<Address> Addresses { get; set; } = [];

    public CompanyCommissions CompanyCommissions { get; set; } = default!;
    public CompanyPreferences CompanyPreferences { get; set; } = default!;

    public CompanySmsPatterns CompanySmsPatterns { get; set; } = default!;
    public CompanyType CompanyType { get; set; } = default!;

    // Navigation Properties

    public Area Country { get; set; } = null!;
    public Area Province { get; set; } = null!;
    public Area City { get; set; } = null!;

    public ICollection<CompanyDomesticPaths> CompanyDomesticPaths { get; set; } = [];

    public ICollection<CompanyInsurance> CompanyInsurances { get; set; } = [];
}