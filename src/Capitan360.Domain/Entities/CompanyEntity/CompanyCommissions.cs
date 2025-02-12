using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyCommissions : Entity
{

    public long Captain360CommissionFromCaptainCargoWebSite { get; set; }
    public long Captain360CommissionFromCompanyWebSite { get; set; }
    public long Captain360CommissionFromCaptainCargoWebService { get; set; }
    public long Captain360CommissionFromCompanyWebService { get; set; }
    public long Captain360CommissionFromCaptainCargoPanel { get; set; }
    public long Captain360CommissionFromCaptainCargoDesktop { get; set; }

    // Navigation Properties
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}