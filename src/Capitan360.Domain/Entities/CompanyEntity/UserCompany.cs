using Capitan360.Domain.Entities.UserEntity;

namespace Capitan360.Domain.Entities.CompanyEntity
{
    public class UserCompany
    {
       

        public string UserId { get; set; }
        public User User { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public DateTime JoinDate { get; set; }



    }
}
