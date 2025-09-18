using Capitan360.Domain.Entities.ManifestForms;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.ManifestForms;

public class ManifestFormConfiguration : BaseEntityConfiguration<ManifestForm>
{
    public override void Configure(EntityTypeBuilder<ManifestForm> builder)
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
               .HasMaxLength(10)
               .IsUnicode()
               .HasColumnType("nvarchar(10)");

        builder.Property(x => x.CompanyReceiverUserInsertedName)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.SourceCountryId)
               .IsRequired(false);
        
        builder.Property(x => x.SourceProvinceId)
               .IsRequired(false);
        
        builder.Property(x => x.SourceCityId)
               .IsRequired(false);

        builder.Property(x => x.DestinationCountryId)
               .IsRequired(false);
        
        builder.Property(x => x.DestinationProvinceId)
               .IsRequired(false);
        
        builder.Property(x => x.DestinationCityId)
               .IsRequired(false);

        builder.Property(x => x.ManifestFormPeriodId)
               .IsRequired(false);

        builder.Property(x => x.Date)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.CompanySenderDescription)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.CompanySenderDescriptionForPrint)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.CompanyReceiverDescription)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.MasterWaybillNo)
               .HasMaxLength(20)
               .IsUnicode()
               .HasColumnType("nvarchar(20)");

        builder.Property(x => x.MasterWaybillWeight)
               .HasColumnType("decimal(18,2)");

        builder.Property(x => x.MasterWaybillAirline)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.MasterWaybillFlightNo)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.MasterWaybillFlightDate)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.State)
               .HasColumnType("smallint");

        builder.Property(x => x.DateIssued)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeIssued)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.DateAirlineDelivery)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeAirlineDelivery)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.DateReceivedAtReceiverCompany)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeReceivedAtReceiverCompany)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.CounterId)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.Dirty)
               .HasColumnType("bit");

        builder.HasOne(x => x.CompanySender)
               .WithMany(c => c.ManifestFormCompanySenders)
               .HasForeignKey(x => x.CompanySenderId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyReceiver)
               .WithMany(c => c.ManifestFormCompanyReceivers)
               .HasForeignKey(x => x.CompanyReceiverId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.ManifestFormPeriod)
               .WithMany(p => p.ManifestForms)
               .HasForeignKey(x => x.ManifestFormPeriodId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceCountry)
               .WithMany(a => a.ManifestFormSourceCountries)
               .HasForeignKey(x => x.SourceCountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceProvince)
               .WithMany(a => a.ManifestFormSourceProvinces)
               .HasForeignKey(x => x.SourceProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceCity)
               .WithMany(a => a.ManifestFormSourceCities)
               .HasForeignKey(x => x.SourceCityId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationCountry)
               .WithMany(a => a.ManifestFormDestinationCountries)
               .HasForeignKey(x => x.DestinationCountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationProvince)
               .WithMany(a => a.ManifestFormDestinationProvinces)
               .HasForeignKey(x => x.DestinationProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationCity)
               .WithMany(a => a.ManifestFormDestinationCities)
               .HasForeignKey(x => x.DestinationCityId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
