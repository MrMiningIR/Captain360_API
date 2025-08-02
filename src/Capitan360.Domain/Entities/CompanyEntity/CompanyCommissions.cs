using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyCommissions : Entity
{
    public long CommissionFromCaptainCargoWebSite { get; set; }
    public long CommissionFromCompanyWebSite { get; set; }
    public long CommissionFromCaptainCargoWebService { get; set; }
    public long CommissionFromCompanyWebService { get; set; }
    public long CommissionFromCaptainCargoPanel { get; set; }
    public long CommissionFromCaptainCargoDesktop { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}