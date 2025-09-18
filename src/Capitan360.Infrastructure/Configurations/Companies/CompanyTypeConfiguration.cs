using Capitan360.Domain.Entities.Companies;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.Companies;

public class CompanyTypeConfiguration : BaseEntityConfiguration<CompanyType>
{
    public override void Configure(EntityTypeBuilder<CompanyType> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.TypeName)
               .IsRequired()
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.DisplayName)
               .IsRequired()
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.HasData(new List<CompanyType>()
        {
            new CompanyType()
            {
                Id = 1,
                DisplayName = "پایه",
                Description = "نوع برای شرکت پایه و اصلی سیستم",
                TypeName = "Master"
            },

            new CompanyType()
             {
                 Id = 2,
                 DisplayName = "بار هوایی",
                 Description = "نوع برای شرکت برای بار هوایی",
                 TypeName = "AirCargo"
             },

            new CompanyType()
            {
                Id = 3,
                DisplayName = "بار زمینی",
                Description = "نوع برای شرکت برای بار زمینی",
                TypeName = "RoadCargo"
            },

            new CompanyType()
            {
                Id = 4,
                DisplayName = "بار اتوبوسی",
                Description = "نوع برای شرکت برای بار اتوبوسی",
                TypeName = "BusCargo"
            }
        });

    }
}