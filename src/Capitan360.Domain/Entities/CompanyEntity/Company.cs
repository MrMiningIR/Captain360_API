using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.AddressEntity;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class Company : Entity
{

    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string EconomicCode { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public string RegistrationId { get; set; } = default!;
    public decimal Tax { get; set; }
    public bool Active { get; set; }
    public string Description { get; set; } = default!;




    // Navigation Properties

    public ICollection<UserCompany> UserCompanies { get; set; } = [];

    public ICollection<CompanyUri> CompanyUris { get; set; } = [];

    public ICollection<CompanyAddress> CompanyAddresses { get; set; } = [];

    public CompanyCommissions CompanyCommissions { get; set; } = default!;
    public CompanyPreferences CompanyPreferences { get; set; } = default!;

    public CompanySmsPatterns CompanySmsPatterns { get; set; } = default!;

}