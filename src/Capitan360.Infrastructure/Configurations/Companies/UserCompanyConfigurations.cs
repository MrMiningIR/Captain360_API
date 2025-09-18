using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class UserCompanyConfigurations : BaseEntityConfiguration<UserCompany>
{
    public override void Configure(EntityTypeBuilder<UserCompany> builder)
    {


        base.Configure(builder);

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
