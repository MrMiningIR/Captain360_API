using Capitan360.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyCommissions : Entity
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