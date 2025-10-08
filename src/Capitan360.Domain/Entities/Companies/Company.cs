using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Entities.CompanyInsurances;
using Capitan360.Domain.Entities.CompanyManifestForms;
using Capitan360.Domain.Entities.Identities;

namespace Capitan360.Domain.Entities.Companies;

public class Company : BaseEntity
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

    public string Description { get; set; } = default!;

    public CompanyCommissions CompanyCommissions { get; set; } = default!;

    public CompanyPreferences CompanyPreferences { get; set; } = default!;

    public CompanySmsPatterns CompanySmsPatterns { get; set; } = default!;

    public ICollection<User> CompanyUsers { get; set; } = [];

    public ICollection<CompanyUri> CompanyUris { get; set; } = [];

    public ICollection<CompanyContentType> CompanyContentTypes { get; set; } = [];

    public ICollection<CompanyPackageType> CompanyPackageTypes { get; set; } = [];

    public ICollection<Address> Addresses { get; set; } = [];

    public ICollection<CompanyDomesticPath> CompanyDomesticPaths { get; set; } = [];

    public ICollection<CompanyDomesticPathReceiverCompany> CompanyDomesticPathReceiverCompanies { get; set; } = [];

    public ICollection<CompanyInsurance> CompanyInsurances { get; set; } = [];

    public ICollection<CompanyBank> CompanyBanks { get; set; } = [];

    public ICollection<CompanyManifestFormPeriod> CompanyManifestFormPeriods { get; set; } = [];

    public ICollection<CompanyManifestForm> CompanyManifestFormCompanySenders { get; set; } = [];

    public ICollection<CompanyManifestForm> CompanyManifestFormCompanyReceivers { get; set; } = [];

    public ICollection<CompanyDomesticWaybillPeriod> CompanyDomesticWaybillPeriods { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillCompanySenders { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillCompanyReceivers { get; set; } = [];
}