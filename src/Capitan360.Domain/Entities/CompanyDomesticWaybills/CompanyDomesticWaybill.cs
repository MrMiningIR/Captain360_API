using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.CompanyInsurances;
using Capitan360.Domain.Entities.CompanyManifestForms;
using Capitan360.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Capitan360.Domain.Entities.CompanyDomesticWaybills;

public class CompanyDomesticWaybill : BaseEntity
{
    public long No { get; set; }

    [ForeignKey(nameof(CompanySender))]
    public int CompanySenderId { get; set; }
    public Company? CompanySender { get; set; }

    [ForeignKey(nameof(CompanyReceiver))]
    public int? CompanyReceiverId { get; set; }
    public Company? CompanyReceiver { get; set; }
    public string? CompanyReceiverUserInsertedCode { get; set; }

    [ForeignKey(nameof(SourceCountry))]
    public int SourceCountryId { get; set; }
    public Area? SourceCountry { get; set; }

    [ForeignKey(nameof(SourceProvince))]
    public int SourceProvinceId { get; set; }
    public Area? SourceProvince { get; set; }

    [ForeignKey(nameof(SourceCity))]
    public int SourceCityId { get; set; }
    public Area? SourceCity { get; set; }

    [ForeignKey(nameof(SourceMunicipalArea))]
    public int? SourceMunicipalAreaId { get; set; }
    public Area? SourceMunicipalArea { get; set; }

    public decimal? SourceLatitude { get; set; }

    public decimal? SourceLongitude { get; set; }

    public string? StopCityName { get; set; }

    [ForeignKey(nameof(DestinationCountry))]
    public int? DestinationCountryId { get; set; }
    public Area? DestinationCountry { get; set; }

    [ForeignKey(nameof(DestinationProvince))]
    public int? DestinationProvinceId { get; set; }
    public Area? DestinationProvince { get; set; }

    [ForeignKey(nameof(DestinationCity))]
    public int? DestinationCityId { get; set; }
    public Area? DestinationCity { get; set; }

    [ForeignKey(nameof(DestinationMunicipalArea))]
    public int? DestinationMunicipalAreaId { get; set; }
    public Area? DestinationMunicipalArea { get; set; }

    public decimal? DestinationLatitude { get; set; }

    public decimal? DestinationLongitude { get; set; }

    [ForeignKey(nameof(CompanyDomesticWaybillPeriod))]
    public int? CompanyDomesticWaybillPeriodId { get; set; }
    public CompanyDomesticWaybillPeriod? CompanyDomesticWaybillPeriod { get; set; }

    [ForeignKey(nameof(CompanyManifestForm))]
    public int? CompanyManifestFormId { get; set; }
    public CompanyManifestForm? CompanyManifestForm { get; set; }

    [ForeignKey(nameof(CompanyInsurance))]
    public int? CompanyInsuranceId { get; set; }
    public CompanyInsurance? CompanyInsurance { get; set; }

    public decimal? GrossWeight { get; set; }

    public decimal? DimensionalWeight { get; set; }

    public decimal? ChargeableWeight { get; set; }

    public int? WeightCount { get; set; }

    public int? Rate { get; set; }

    public decimal? CompanyDomesticWaybillTax { get; set; }

    public long? ExitFare { get; set; }

    public long? ExitStampBill { get; set; }

    public long? ExitPackaging { get; set; }

    public long? ExitAccumulation { get; set; }

    public long? ExitDistribution { get; set; }

    public long? ExitExtraSource { get; set; }

    public long? ExitExtraDestination { get; set; }

    public long? ExitPricing { get; set; }

    public long? ExitRevenue1 { get; set; }

    public long? ExitRevenue2 { get; set; }

    public long? ExitRevenue3 { get; set; }

    public long? ExitTaxCompanySender { get; set; }

    public long? ExitTaxCompanyReceiver { get; set; }

    public long? ExitInsuranceCost { get; set; }

    public long? ExitTaxInsuranceCost { get; set; }

    public long? ExitInsuranceCostGain { get; set; }

    public long? ExitTaxInsuranceCostGain { get; set; }

    public long? ExitDiscount { get; set; }

    public long? EntranceDiscount { get; set; }

    public long? ExitTotalCost { get; set; }

    public string? HandlingInformation { get; set; }

    public string? FlightNo { get; set; }

    public string? FlightDate { get; set; }

    public string? CompanySenderDateFinancial { get; set; }

    public bool? CompanySenderCashPayment { get; set; }

    public bool? CompanySenderCashOnDelivery { get; set; }

    public bool? CompanySenderBankPayment { get; set; }

    [ForeignKey(nameof(CompanySenderBank))]
    public int? CompanySenderBankId { get; set; }
    public CompanyBank? CompanySenderBank { get; set; }

    public string? CompanySenderBankPaymentNo { get; set; }

    public bool? CompanySenderCreditPayment { get; set; }

    [ForeignKey(nameof(CustomerPanel))]
    public string? CustomerPanelId { get; set; }
    public User? CustomerPanel { get; set; }

    public string? CompanyReceiverDateFinancial { get; set; }

    public bool? CompanyReceiverCashPayment { get; set; }

    public bool? CompanyReceiverBankPayment { get; set; }

    public bool? CompanyReceiverCashOnDelivery { get; set; }

    [ForeignKey(nameof(CompanyReceiverBank))]
    public int? CompanyReceiverBankId { get; set; }
    public CompanyBank? CompanyReceiverBank { get; set; }

    public string? CompanyReceiverBankPaymentNo { get; set; }

    public bool? CompanyReceiverCreditPayment { get; set; }

    [ForeignKey(nameof(CompanyReceiverResponsibleCustomer))]
    public string? CompanyReceiverResponsibleCustomerId { get; set; }
    public User? CompanyReceiverResponsibleCustomer { get; set; }

    public string? CustomerSenderNameFamily { get; set; }

    public string? CustomerSenderMobile { get; set; }

    public string? CustomerSenderAddress { get; set; }

    public short? TypeOfFactorInSamanehMoadianId { get; set; }

    public string? CustomerSenderNationalCode { get; set; }

    public string? CustomerSenderEconomicCode { get; set; }

    public string? CustomerSenderNationalID { get; set; }

    public string? CustomerReceiverNameFamily { get; set; }

    public string? CustomerReceiverMobile { get; set; }

    public string? CustomerReceiverAddress { get; set; }

    public short State { get; set; }

    public string? DateIssued { get; set; }

    public string? TimeIssued { get; set; }

    public string? DateCollectiong { get; set; }

    public string? TimeCollectiong { get; set; }

    [ForeignKey(nameof(BikeDeliveryInSenderCompany))]
    public string? BikeDeliveryInSenderCompanyId { get; set; }
    public User? BikeDeliveryInSenderCompany { get; set; }
    public string? BikeDeliveryInSenderCompanyAgent { get; set; }

    public string? DateReceivedAtSenderCompany { get; set; }

    public string? TimeReceivedAtSenderCompany { get; set; }

    public string? DateManifested { get; set; }

    public string? TimeManifested { get; set; }

    public string? DateAirlineDelivery { get; set; }

    public string? TimeAirlineDelivery { get; set; }

    public string? DateReceivedAtReceiverCompany { get; set; }

    public string? TimeReceivedAtReceiverCompany { get; set; }

    public string? DateDistribution { get; set; }

    public string? TimeDistribution { get; set; }

    [ForeignKey(nameof(BikeDeliveryInReceiverCompany))]
    public string? BikeDeliveryInReceiverCompanyId { get; set; }
    public User? BikeDeliveryInReceiverCompany { get; set; }
    public string? BikeDeliveryInReceiverCompanyAgent { get; set; }

    public string? DateDelivery { get; set; }

    public string? TimeDelivery { get; set; }

    public string? EntranceDeliveryPerson { get; set; }

    public string? EntranceTransfereePersonName { get; set; }

    public string? EntranceTransfereePersonNationalCode { get; set; }

    public string? DescriptionSenderCompany { get; set; }

    public string? DescriptionReceiverCompany { get; set; }

    public string? DescriptionSenderCustomer { get; set; }

    public string? DescriptionReceiverCustomer { get; set; }

    public bool? Lock { get; set; }

    public bool? IsIssueFromCaptainCargoWebSite { get; set; }

    public bool? IsIssueFromCompanyWebSite { get; set; }

    public bool? IsIssueFromCaptainCargoWebService { get; set; }

    public bool? IsIssueFromCompanyWebService { get; set; }

    public bool? IsIssueFromCaptainCargoPanel { get; set; }

    public bool? IsIssueFromCaptainCargoDesktop { get; set; }

    public long? CaptainCargoPrice { get; set; }

    [ForeignKey(nameof(Counter))]
    public string? CounterId { get; set; }
    public User? Counter { get; set; }

    public bool? Dirty { get; set; }

    public ICollection<CompanyDomesticWaybillPackageType> CompanyDomesticWaybillPackageTypes { get; set; } = [];
}
