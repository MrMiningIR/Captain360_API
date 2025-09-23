using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Capitan360.Domain.Entities.CompanyInsurances;

public class CompanyInsurance : BaseEntity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string CaptainCargoCode { get; set; } = default!;

    public decimal Tax { get; set; }

    public long Scale { get; set; }

    public bool Active { get; set; }

    public string Description { get; set; } = default!;

    public ICollection<CompanyInsuranceCharge> CompanyInsuranceCharges { get; set; } = [];

    public ICollection<CompanyDomesticWaybill> CompanyDomesticWaybills { get; set; } = [];
}