using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyInsurance : Entity
{
    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public string Code { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string? CaptainCargoCode { get; set; }

    public decimal Tax { get; set; }

    public long Scale { get; set; }

    public bool Active { get; set; }

    public string? Description { get; set; }

    public ICollection<CompanyInsuranceCharge> CompanyInsuranceCharges { get; set; } = [];
}