using Capitan360.Domain.Entities.CompanyDomesticWaybills;
using Capitan360.Domain.Enums;
using Capitan360.Infrastructure.Configurations.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Capitan360.Infrastructure.Configurations.CompanyDomesticWaybills;

public class CompanyDomesticWaybillConfiguration : BaseEntityConfiguration<CompanyDomesticWaybill>
{
    public override void Configure(EntityTypeBuilder<CompanyDomesticWaybill> builder)
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

        builder.Property(x => x.SourceCountryId)
                .IsRequired();

        builder.Property(x => x.SourceProvinceId)
               .IsRequired();

        builder.Property(x => x.SourceCityId)
              .IsRequired();

        builder.Property(x => x.SourceMunicipalAreaId)
               .IsRequired(false);

        builder.Property(x => x.SourceLatitude)
               .IsRequired(false)
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.SourceLongitude)
               .IsRequired(false)
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.StopCityName)
               .IsRequired(false)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.DestinationCountryId)
               .IsRequired(false);

        builder.Property(x => x.DestinationProvinceId)
               .IsRequired(false);

        builder.Property(x => x.DestinationCityId)
               .IsRequired(false);

        builder.Property(x => x.DestinationMunicipalAreaId)
               .IsRequired(false);

        builder.Property(x => x.DestinationLatitude)
               .IsRequired(false)
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.DestinationLongitude)
               .IsRequired(false)
               .HasColumnType("decimal(9,6)");

        builder.Property(x => x.CompanyDomesticWaybillPeriodId)
               .IsRequired(false);

        builder.Property(x => x.CompanyManifestFormId)
               .IsRequired(false);

        builder.Property(x => x.CompanyInsuranceId)
               .IsRequired(false);

        builder.Property(x => x.GrossWeight)
               .IsRequired(false)
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.DimensionalWeight)
               .IsRequired(false)
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.ChargeableWeight)
               .IsRequired(false)
               .HasColumnType("decimal(10,2)");

        builder.Property(x => x.WeightCount)
               .IsRequired(false);

        builder.Property(x => x.Rate)
               .IsRequired(false);

        builder.Property(x => x.CompanyDomesticWaybillTax)
               .IsRequired(false)
               .HasColumnType("decimal(5,2)");

        builder.Property(x => x.ExitFare)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitStampBill)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitPackaging)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitAccumulation)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitDistribution)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitExtraSource)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitExtraDestination)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitPricing)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitRevenue1)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitRevenue2)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitRevenue3)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitTaxCompanySender)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitTaxCompanyReceiver)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitInsuranceCost)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitTaxInsuranceCost)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitInsuranceCostGain)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitTaxInsuranceCostGain)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitDiscount)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.EntranceDiscount)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.ExitTotalCost)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.HandlingInformation)
               .IsRequired(false)
               .HasMaxLength(4000)
               .IsUnicode()
               .HasColumnType("nvarchar(4000)");

        builder.Property(x => x.FlightNo)
               .IsRequired(false)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.FlightDate)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.CompanySenderDateFinancial)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.CompanySenderCashPayment)
               .IsRequired(false);

        builder.Property(x => x.CompanySenderCashOnDelivery)
               .IsRequired(false);

        builder.Property(x => x.CompanySenderBankId)
               .IsRequired(false);

        builder.Property(x => x.CompanySenderCreditPayment)
               .IsRequired(false);

        builder.Property(x => x.CompanySenderBankPaymentNo)
               .IsRequired(false)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CustomerPanelId)
               .IsRequired(false)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.CompanyReceiverDateFinancial)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.CompanyReceiverCashPayment)
               .IsRequired(false);

        builder.Property(x => x.CompanyReceiverBankPayment)
               .IsRequired(false);

        builder.Property(x => x.CompanyReceiverCashOnDelivery)
               .IsRequired(false);

        builder.Property(x => x.CompanyReceiverBankId)
               .IsRequired(false);

        builder.Property(x => x.CompanyReceiverBankPaymentNo)
               .IsRequired(false)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CompanyReceiverCreditPayment)
               .IsRequired(false);

        builder.Property(x => x.CompanyReceiverResponsibleCustomerId)
               .IsRequired(false)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.CustomerSenderNameFamily)
               .IsRequired(false)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.CustomerSenderMobile)
               .IsRequired(false)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.CustomerSenderAddress)
               .IsRequired(false)
               .HasMaxLength(1000)
               .IsUnicode()
               .HasColumnType("nvarchar(1000)");

        builder.Property(x => x.TypeOfFactorInSamanehMoadianId)
               .IsRequired(false)
               .HasColumnType("smallint");

        builder.Property(x => x.CustomerSenderNationalCode)
               .IsRequired(false)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CustomerSenderEconomicCode)
               .IsRequired(false)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CustomerSenderNationalID)
               .IsRequired(false)
               .HasMaxLength(50)
               .IsUnicode()
               .HasColumnType("nvarchar(50)");

        builder.Property(x => x.CustomerReceiverNameFamily)
               .IsRequired(false)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.CustomerReceiverMobile)
               .IsRequired(false)
               .HasMaxLength(30)
               .IsUnicode()
               .HasColumnType("nvarchar(30)");

        builder.Property(x => x.CustomerReceiverAddress)
               .IsRequired(false)
               .HasMaxLength(1000)
               .IsUnicode()
               .HasColumnType("nvarchar(1000)");

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

        builder.Property(x => x.DateCollectiong)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeCollectiong)
               .IsRequired(false)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.BikeDeliveryInSenderCompanyId)
               .IsRequired(false)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.BikeDeliveryInSenderCompanyAgent)
               .IsRequired(false)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.DateReceivedAtSenderCompany)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeReceivedAtSenderCompany)
               .IsRequired(false)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.DateManifested)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeManifested)
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

        builder.Property(x => x.DateDistribution)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeDistribution)
               .IsRequired(false)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.BikeDeliveryInReceiverCompanyId)
               .IsRequired(false)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.Property(x => x.BikeDeliveryInReceiverCompanyAgent)
               .IsRequired(false)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.DateDelivery)
               .IsRequired(false)
               .HasColumnType("char(10)")
               .IsUnicode(false);

        builder.Property(x => x.TimeDelivery)
               .IsRequired(false)
               .HasColumnType("char(5)")
               .IsUnicode(false);

        builder.Property(x => x.EntranceDeliveryPerson)
               .IsRequired(false)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.EntranceTransfereePersonName)
               .IsRequired(false)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.EntranceTransfereePersonNationalCode)
               .IsRequired(false)
               .HasMaxLength(100)
               .IsUnicode()
               .HasColumnType("nvarchar(100)");

        builder.Property(x => x.DescriptionSenderCompany)
               .IsRequired(false)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.DescriptionReceiverCompany)
               .IsRequired(false)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.DescriptionSenderCustomer)
               .IsRequired(false)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.DescriptionReceiverCustomer)
               .IsRequired(false)
               .HasMaxLength(500)
               .IsUnicode()
               .HasColumnType("nvarchar(500)");

        builder.Property(x => x.Lock)
               .IsRequired(false);

        builder.Property(x => x.IsIssueFromCaptainCargoWebSite)
               .IsRequired(false);

        builder.Property(x => x.IsIssueFromCompanyWebSite)
               .IsRequired(false);

        builder.Property(x => x.IsIssueFromCaptainCargoWebService)
               .IsRequired(false);

        builder.Property(x => x.IsIssueFromCompanyWebService)
               .IsRequired(false);

        builder.Property(x => x.IsIssueFromCaptainCargoPanel)
               .IsRequired(false);

        builder.Property(x => x.IsIssueFromCaptainCargoDesktop)
               .IsRequired(false);

        builder.Property(x => x.CaptainCargoPrice)
               .IsRequired(false)
               .HasColumnType("bigint");

        builder.Property(x => x.Dirty)
               .IsRequired(false)
               .HasColumnType("bit"); 

        builder.Property(x => x.CounterId)
               .IsRequired(false)
               .HasMaxLength(450)
               .IsUnicode()
               .HasColumnType("nvarchar(450)");

        builder.HasOne(x => x.CompanySender)
               .WithMany(c => c.CompanyDomesticWaybillCompanySenders)
               .HasForeignKey(x => x.CompanySenderId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyReceiver)
               .WithMany(c => c.CompanyDomesticWaybillCompanyReceivers)
               .HasForeignKey(x => x.CompanyReceiverId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyManifestForm)
               .WithMany(mf => mf.CompanyDomesticWaybills)
               .HasForeignKey(x => x.CompanyManifestFormId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyDomesticWaybillPeriod)
               .WithMany(p => p.CompanyDomesticWaybills)
               .HasForeignKey(x => x.CompanyDomesticWaybillPeriodId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyInsurance)
               .WithMany(ci => ci.CompanyDomesticWaybills)
               .HasForeignKey(x => x.CompanyInsuranceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceCountry)
               .WithMany(a => a.CompanyDomesticWaybillSourceCountries)
               .HasForeignKey(x => x.SourceCountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceProvince)
               .WithMany(a => a.CompanyDomesticWaybillSourceProvinces)
               .HasForeignKey(x => x.SourceProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceCity)
               .WithMany(a => a.CompanyDomesticWaybillSourceCities)
               .HasForeignKey(x => x.SourceCityId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SourceMunicipalArea)
               .WithMany(a => a.CompanyDomesticWaybillSourceMunicipalAreas)
               .HasForeignKey(x => x.SourceMunicipalAreaId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationCountry)
               .WithMany(a => a.CompanyDomesticWaybillDestinationCountries)
               .HasForeignKey(x => x.DestinationCountryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationProvince)
               .WithMany(a => a.CompanyDomesticWaybillDestinationProvinces)
               .HasForeignKey(x => x.DestinationProvinceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationCity)
               .WithMany(a => a.CompanyDomesticWaybillDestinationCities)
               .HasForeignKey(x => x.DestinationCityId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.DestinationMunicipalArea)
               .WithMany(a => a.CompanyDomesticWaybillDestinationMunicipalAreas)
               .HasForeignKey(x => x.DestinationMunicipalAreaId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanySenderBank)
               .WithMany(cb => cb.CompanyDomesticWaybillCompanyBankSenders)
               .HasForeignKey(x => x.CompanySenderBankId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.CompanyReceiverBank)
               .WithMany(cb => cb.CompanyDomesticWaybillCompanyBankReceivers)
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
