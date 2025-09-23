using Capitan360.Domain.Entities.CompanyManifestForms;
using Capitan360.Domain.Enums;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyManifestForms;

public class CompanyManifestFormConfiguration : BaseEntityConfiguration<CompanyManifestForm>
{
    public override void Configure(EntityTypeBuilder<CompanyManifestForm> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.No)
               .IsRequired()
               .HasColumnType("bigint");

        builder.Property(x => x.CompanySenderId)
               .IsRequired();
        
        builder.Property(x => x.CompanyReceiverId)
               .IsRequired(false);

        builder.Property(x => x.CompanyReceiverUserInsertedCode)
               .IsRequired(false)
               .HasMaxLength(10)
               .IsUnicode()
               .HasColumnType("nvarchar(10)");

        builder.Property(x => x.CompanyReceiverUserInsertedName)
               .IsRequired(false)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.SourceCountryId)
               .IsRequired();

        builder.Property(x => x.SourceProvinceId)
               .IsRequired();

        builder.Property(x => x.SourceCityId)
               .IsRequired();

        builder.Property(x => x.DestinationCountryId)
               .IsRequired(false);

        builder.Property(x => x.DestinationProvinceId)
               .IsRequired(false);

        builder.Property(x => x.DestinationCityId)
               .IsRequired(false);

        builder.Property(x => x.CompanyManifestFormPeriodId)
               .IsRequired(false);

        builder.Property(x => x.Date)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.CompanySenderDescription)
               .IsUnicode(false)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.CompanySenderDescriptionForPrint)
               .IsUnicode(false)
              .HasMaxLength(500)
              .IsUnicode()
              .HasColumnType("nvarchar(500)");

        builder.Property(x => x.CompanyReceiverDescription)
               .IsUnicode(false)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.MasterWaybillNo)
               .IsUnicode(false)
               .HasMaxLength(20)
               .IsUnicode()
               .HasColumnType("nvarchar(20)");

        builder.Property(x => x.MasterWaybillWeight)
               .IsUnicode(false)
               .HasColumnType("decimal(18,2)");

        builder.Property(x => x.MasterWaybillAirline)
               .IsUnicode(false)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.MasterWaybillFlightNo)
               .IsUnicode(false)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.MasterWaybillFlightDate)
               .IsRequired(false)
               .IsUnicode(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.State)
               .IsRequired()
               .HasColumnType("smallint");

        builder.Property(x => x.DateIssued)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeIssued)
               .IsRequired(false)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.DateAirlineDelivery)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeAirlineDelivery)
               .IsRequired(false)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.DateReceivedAtReceiverCompany)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeReceivedAtReceiverCompany)
               .IsRequired(false)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.CounterId)
               .IsRequired(false)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.Dirty)
               .IsRequired(false)
               .HasColumnType("bit");

        builder.HasOne(x => x.CompanySender)
               .WithMany(c => c.CompanyManifestFormCompanySenders)
               .HasForeignKey(x => x.CompanySenderId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyReceiver)
               .WithMany(c => c.CompanyManifestFormCompanyReceivers)
               .HasForeignKey(x => x.CompanyReceiverId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyManifestFormPeriod)
               .WithMany(p => p.CompanyManifestForms)
               .HasForeignKey(x => x.CompanyManifestFormPeriodId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceCountry)
               .WithMany(a => a.CompanyManifestFormSourceCountries)
               .HasForeignKey(x => x.SourceCountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceProvince)
               .WithMany(a => a.CompanyManifestFormSourceProvinces)
               .HasForeignKey(x => x.SourceProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceCity)
               .WithMany(a => a.CompanyManifestFormSourceCities)
               .HasForeignKey(x => x.SourceCityId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationCountry)
               .WithMany(a => a.CompanyManifestFormDestinationCountries)
               .HasForeignKey(x => x.DestinationCountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationProvince)
               .WithMany(a => a.CompanyManifestFormDestinationProvinces)
               .HasForeignKey(x => x.DestinationProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationCity)
               .WithMany(a => a.CompanyManifestFormDestinationCities)
               .HasForeignKey(x => x.DestinationCityId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
