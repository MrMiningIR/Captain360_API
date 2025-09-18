using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyCommissions : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public long CommissionFromCaptainCargoWebSite { get; set; }

    public long CommissionFromCompanyWebSite { get; set; }

    public long CommissionFromCaptainCargoWebService { get; set; }

    public long CommissionFromCompanyWebService { get; set; }

    public long CommissionFromCaptainCargoPanel { get; set; }

    public long CommissionFromCaptainCargoDesktop { get; set; }
}