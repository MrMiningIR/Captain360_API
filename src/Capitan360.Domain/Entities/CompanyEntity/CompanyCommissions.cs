using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyCommissions : Entity
{

    public decimal CommissionFromCaptainCargoWebSite { get; set; }
    public decimal CommissionFromCompanyWebSite { get; set; }
    public decimal CommissionFromCaptainCargoWebService { get; set; }
    public decimal CommissionFromCompanyWebService { get; set; }
    public decimal CommissionFromCaptainCargoPanel { get; set; }
    public decimal CommissionFromCaptainCargoDesktop { get; set; }

    // Navigation Properties
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}