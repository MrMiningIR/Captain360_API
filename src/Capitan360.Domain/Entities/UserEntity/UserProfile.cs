
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;

namespace Capitan360.Domain.Entities.UserEntity;
public class UserProfile: Entity
{
    

     public string? TelegramPhoneNumber { get; set; }
     public string? TellNumber { get; set; }
     public string NationalCode { get; set; }
     public string EconomicCode { get; set; }
     public string NationalId { get; set; }
     public string RegistrationId { get; set; }
     public string Description { get; set; }
     public MoadianFactorType MoadianFactorType { get; set; }
     public bool IsBikeDelivery { get; set; }
     public string RecoveryPasswordCode { get; set; }
     public DateTime RecoveryPasswordCodeExpireTime { get; set; }

     public decimal Credit { get; set; }
     public bool HasCredit { get; set; }


    // Foreign Key to User
    public string UserId { get; set; }
     public User?  User { get; set; }


 }