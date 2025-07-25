using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.AuthorizationEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.AuthorizationConfigs;

internal class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(ConstantNames.RoleTableName);


        builder.Property(x => x.PersianName).HasMaxLength(150).IsRequired(false);

        builder.HasMany(r => r.RolePermissions)
            .WithOne(rp => rp.Role)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new List<Role>()
        {

                new ()
                {
                    Id = "bcde120b-62bf-4268-b51d-ef55faffce4d",
                    PersianName = "مدیرکل",
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                },
              new ()
              {
                  Id = "c09f590c-733e-470c-b6c7-cbc4fb362715",
                  PersianName = "مدیر",
                  Name = "Manager",
                  NormalizedName = "MANAGER"
              },
                new ()
              {
                  Id = "f837dfac-767c-48aa-8869-8ad6f109ca5e",
                  PersianName = "کاربر",
                  Name = "User",
                  NormalizedName = "USER"
              }




        });
    }
}