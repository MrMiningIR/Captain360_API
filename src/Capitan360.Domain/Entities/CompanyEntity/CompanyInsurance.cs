using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class CompanyInsurance : Entity
{

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Tax { get; set; }
    public decimal Scale { get; set; }
    public string Description { get; set; } = default!;
    public bool Active { get; set; }

    [ForeignKey(nameof(CompanyType))]
    public int CompanyTypeId { get; set; }
    public CompanyType? CompanyType { get; set; }

    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }


    public ICollection<CompanyInsuranceCharge> CompanyInsuranceCharges { get; set; } = [];
    //public CompanyInsuranceCharge? CompanyInsuranceCharge { get; set; }

}