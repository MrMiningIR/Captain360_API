using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs
{
    internal class UserCompanyConfigurations : IEntityTypeConfiguration<UserCompany>
    {
        public void Configure(EntityTypeBuilder<UserCompany> builder)
        {
            builder.HasKey(uc => uc.Id);

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.UserCompanies)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                 .HasOne(uc => uc.Company)
                 .WithMany(c => c.UserCompanies)
                 .HasForeignKey(uc => uc.CompanyId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.Property(uc => uc.JoinDate).HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAdd();

            // builder
            //.HasIndex(uc => uc.UserId)
            //.IsUnique();


            builder
            .HasIndex(uc => new { uc.UserId, uc.CompanyId })
            .HasDatabaseName("IX_UserCompany_Active")
            .IsUnique()
            .HasFilter("[Deleted] = 0");

          

        }
    }
}
