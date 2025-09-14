using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.AddressEntity;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class Company : Entity
{
    [ForeignKey(nameof(CompanyType))]
    public int CompanyTypeId { get; set; }
    public CompanyType? CompanyType { get; set; }

    [ForeignKey(nameof(Country))]
    public int CountryId { get; set; }
    public Area? Country { get; set; }

    [ForeignKey(nameof(Province))]
    public int ProvinceId { get; set; }
    public Area? Province { get; set; }

    [ForeignKey(nameof(City))]
    public int CityId { get; set; }
    public Area? City { get; set; }

    public string Code { get; set; } = default!;

    public string MobileCounter { get; set; } = default!;

    public string Name { get; set; } = default!;

    public bool IsParentCompany { get; set; }

    public bool Active { get; set; }

    public string? Description { get; set; }

    public CompanyCommissions CompanyCommissions { get; set; } = default!;

    public CompanyPreferences CompanyPreferences { get; set; } = default!;

    public CompanySmsPatterns CompanySmsPatterns { get; set; } = default!;

    public ICollection<UserCompany> UserCompanies { get; set; } = [];

    public ICollection<CompanyUri> CompanyUris { get; set; } = [];

    public ICollection<CompanyContentType> CompanyContentTypes { get; set; } = [];

    public ICollection<CompanyPackageType> CompanyPackageTypes { get; set; } = [];

    public ICollection<Address> Addresses { get; set; } = [];

    public ICollection<CompanyDomesticPaths> CompanyDomesticPaths { get; set; } = [];

    public ICollection<CompanyInsurance> CompanyInsurances { get; set; } = [];
}