using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;

namespace Capitan360.Domain.Entities.Companies;

public class CompanyBank : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public bool Active { get; set; }

    public int Order { get; set; }

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillCompanyBankSenders { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybillCompanyBankReceivers { get; set; } = [];
}
