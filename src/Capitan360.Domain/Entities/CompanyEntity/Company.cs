using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.CompanyEntity;

public class Company: Entity
{

    public string Name { get; set; } = default!;
  

  

    public ICollection<UserCompany> UserCompanies { get; set; } = [];

    public ICollection<CompanyUri>  CompanyUris { get; set; } = [];
}