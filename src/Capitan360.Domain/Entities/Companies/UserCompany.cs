using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.Users;

namespace Capitan360.Domain.Entities.Companies;

public class UserCompany : BaseEntity
{


    public string UserId { get; set; }
    public User User { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public DateTime JoinDate { get; set; }



}
