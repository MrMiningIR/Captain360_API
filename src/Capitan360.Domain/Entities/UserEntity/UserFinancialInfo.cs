



using Capitan360.Domain.Abstractions;

namespace Capitan360.Domain.Entities.UserEntity;

public class UserFinancialInfo: Entity
{
   
    public decimal Credit { get; set; }
    public bool HasCredit { get; set; }


    // Foreign Key to User
    public string  UserId { get; set; }
    public User? User { get; set; }
}