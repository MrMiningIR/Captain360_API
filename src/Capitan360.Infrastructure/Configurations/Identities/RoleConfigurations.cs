using Capitan360.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Identities;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.PersianName)
              .IsRequired()
              .HasMaxLength(50)
              .IsUnicode()
              .HasColumnType("nvarchar(50)");

        builder.Property(x => x.Visible)
               .IsRequired();

    builder.HasData(new List<Role>()
        {
            new ()
            {
                Id = "bcde120b-62bf-4268-b51d-ef55faffce4d",
                PersianName = "مدیرکل",
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                Visible = false
            },
            new ()
            {
                Id = "c09f590c-733e-470c-b6c7-cbc4fb362715",
                PersianName = "مدیر",
                Name = "Manager",
                NormalizedName = "MANAGER",
                Visible = true
            },
            new ()
            {
                Id = "f837dfac-767c-48aa-8869-8ad6f109ca5e",
                PersianName = "کاربر",
                Name = "User",
                NormalizedName = "USER",
                Visible = true
            }
        });
    }
}