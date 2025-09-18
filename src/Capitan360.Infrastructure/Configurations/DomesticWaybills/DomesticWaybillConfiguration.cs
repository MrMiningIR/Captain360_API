using Capitan360.Domain.Entities.DomesticWaybills;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.DomesticWaybills;

public class DomesticWaybillConfiguration : BaseEntityConfiguration<DomesticWaybill>
{
    public override void Configure(EntityTypeBuilder<DomesticWaybill> builder)
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

        builder.Property(x => x.SourceMunicipalAreaId)
               .IsRequired(false);

        builder.Property(x => x.SourceLatitude)
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.SourceLongitude)
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.StopCityName)
               .HasMaxLength(30).IsUnicode().HasColumnType("nvarchar(30)");

        builder.Property(x => x.DestinationCountryId)
               .IsRequired(false);

        builder.Property(x => x.DestinationProvinceId)
               .IsRequired(false);

        builder.Property(x => x.DestinationCityId)
               .IsRequired(false);

        builder.Property(x => x.DestinationMunicipalAreaId)
               .IsRequired(false);

        builder.Property(x => x.DestinationLatitude)
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.DestinationLongitude)
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.DomesticWaybillPeriodId)
               .IsRequired(false);

        builder.Property(x => x.ManifestFormId)
               .IsRequired(false);

        builder.Property(x => x.CompanyInsuranceId)
               .IsRequired(false);

        builder.Property(x => x.GrossWeight)
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.DimensionalWeight)
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.ChargeableWeight)
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.DomesticWaybillTax)
               .HasColumnType("decimal(5,2)");

        builder.Property(x => x.ExitFare)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitStampBill)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitPackaging)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitAccumulation)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitDistribution)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitExtraSource)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitExtraDestination)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitPricing)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitRevenue1)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitRevenue2)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitRevenue3)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitTaxCompanySender)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitTaxCompanyReceiver)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitInsuranceCost)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitTaxInsuranceCost)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitInsuranceCostGain)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitTaxInsuranceCostGain)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitDiscount)
            .HasColumnType("bigint");

        builder.Property(x => x.ExitTotalCost)
            .HasColumnType("bigint");

        builder.Property(x => x.HandlingInformation)
               .HasMaxLength(4000)
               .IsUnicode()
               .HasColumnType("nvarchar(4000)");

        builder.Property(x => x.FlightNo)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.FlightDate)
            .HasColumnType("char(10)")
            .IsUnicode(false);

        builder.Property(x => x.CompanySenderDateFinancial)
            .HasColumnType("char(10)")
            .IsUnicode(false);

        builder.Property(x => x.CompanySenderBankPaymentNo)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CustomerPanelId) 
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.CompanyReceiverDateFinancial)
            .HasColumnType("char(10)")
            .IsUnicode(false);

        builder.Property(x => x.CompanyReceiverBankPaymentNo)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CompanyReceiverResponsibleCustomerId)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.CustomerSenderNameFamily)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.CustomerSenderMobile)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.CustomerSenderAddress)
               .HasMaxLength(1000)
               .IsUnicode()
               .HasColumnType("nvarchar(1000)");

        builder.Property(x => x.TypeOfFactorInSamanehMoadianId)
               .HasColumnType("smallint");

        builder.Property(x => x.CustomerSenderNationalCode)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CustomerSenderEconomicCode)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CustomerSenderNationalID)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CustomerReceiverNameFamily)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.CustomerReceiverMobile)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.CustomerReceiverAddress)
               .HasMaxLength(1000)
               .IsUnicode()
               .HasColumnType("nvarchar(1000)");

        builder.Property(x => x.State)
               .HasColumnType("smallint");

        builder.Property(x => x.DateIssued)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeIssued)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.DateCollectiong)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeCollectiong)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.BikeDeliveryInSenderCompanyId)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.BikeDeliveryInSenderCompanyAgent)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.DateReceivedAtSenderCompany)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeReceivedAtSenderCompany)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.DateManifested)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeManifested)
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

        builder.Property(x => x.DateDistribution)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeDistribution)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.BikeDeliveryInReceiverCompanyId)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.BikeDeliveryInReceiverCompanyAgent)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.DateDelivery)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeDelivery)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.EntranceDeliveryPerson)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.EntranceTransfereePersonName)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.EntranceTransfereePersonNationalCode)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.DescriptionSenderComapny)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.DescriptionReceiverCompany)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.DescriptionSenderCustomer)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.DescriptionReceiverCustomer)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.CaptainCargoPrice)
               .HasColumnType("bigint");

        builder.Property(x => x.CounterId)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.HasOne(x => x.CompanySender)
               .WithMany(c => c.DomesticWaybillCompanySenders)
               .HasForeignKey(x => x.CompanySenderId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyReceiver)
               .WithMany(c => c.DomesticWaybillCompanyReceivers)
               .HasForeignKey(x => x.CompanyReceiverId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.ManifestForm)
               .WithMany(mf => mf.DomesticWaybills)
               .HasForeignKey(x => x.ManifestFormId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DomesticWaybillPeriod)
               .WithMany(p => p.DomesticWaybills)
               .HasForeignKey(x => x.DomesticWaybillPeriodId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyInsurance)
               .WithMany(ci => ci.DomesticWaybills)
               .HasForeignKey(x => x.CompanyInsuranceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceCountry)
               .WithMany(a => a.DomesticWaybillSourceCountries)
               .HasForeignKey(x => x.SourceCountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceProvince)
               .WithMany(a => a.DomesticWaybillSourceProvinces)
               .HasForeignKey(x => x.SourceProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceCity)
               .WithMany(a => a.DomesticWaybillSourceCities)
               .HasForeignKey(x => x.SourceCityId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceMunicipalArea)
               .WithMany(a => a.DomesticWaybillSourceMunicipalAreas)
               .HasForeignKey(x => x.SourceMunicipalAreaId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationCountry)
               .WithMany(a => a.DomesticWaybillDestinationCountries)
               .HasForeignKey(x => x.DestinationCountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationProvince)
               .WithMany(a => a.DomesticWaybillDestinationProvinces)
               .HasForeignKey(x => x.DestinationProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationCity)
               .WithMany(a => a.DomesticWaybillDestinationCities)
               .HasForeignKey(x => x.DestinationCityId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationMunicipalArea)
               .WithMany(a => a.DomesticWaybillDestinationMunicipalAreas)
               .HasForeignKey(x => x.DestinationMunicipalAreaId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanySenderBank)
               .WithMany(cb => cb.DomesticWaybillCompanyBankSenders)
               .HasForeignKey(x => x.CompanySenderBankId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyReceiverBank)
               .WithMany(cb => cb.DomesticWaybillCompanyBankReceivers)
               .HasForeignKey(x => x.CompanyReceiverBankId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CustomerPanel)
               .WithMany() 
               .HasForeignKey(x => x.CustomerPanelId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyReceiverResponsibleCustomer)
               .WithMany()
               .HasForeignKey(x => x.CompanyReceiverResponsibleCustomerId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.BikeDeliveryInSenderCompany)
               .WithMany()
               .HasForeignKey(x => x.BikeDeliveryInSenderCompanyId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.BikeDeliveryInReceiverCompany)
               .WithMany()
               .HasForeignKey(x => x.BikeDeliveryInReceiverCompanyId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Counter)
               .WithMany()
               .HasForeignKey(x => x.CounterId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
