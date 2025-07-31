using Capitan360.Domain.Entities.CompanyEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyConfigs;

public class CompanyTypeConfigurations : BaseEntityConfiguration<CompanyType>
{
    public override void Configure(EntityTypeBuilder<CompanyType> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.TypeName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(150);

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
    TypeName = "LandCargo"
}
        });
    }
}