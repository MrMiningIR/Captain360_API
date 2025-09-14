using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.Users;
using Capitan360.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Capitan360.Domain.Entities.ManifestForms;

public class ManifestForm : BaseEntity
{
    public long No { get; set; }

    [ForeignKey(nameof(CompanySender))]
    public int CompanySenderId { get; set; }
    public Company? CompanySender { get; set; }

    [ForeignKey(nameof(CompanyReceiver))]
    public int? CompanyReceiverId { get; set; }
    public Company? CompanyReceiver { get; set; }
    public string? CompanyReceiverUserInsertedCode { get; set; }
    public string? CompanyReceiverUserInsertedName { get; set; }

    [ForeignKey(nameof(SourceCountry))]
    public int? SourceCountryId { get; set; }
    public Area? SourceCountry { get; set; }

    [ForeignKey(nameof(SourceProvince))]
    public int? SourceProvinceId { get; set; }
    public Area? SourceProvince { get; set; }

    [ForeignKey(nameof(SourceCity))]
    public int? SourceCityId { get; set; }
    public Area? SourceCity { get; set; }

    [ForeignKey(nameof(DestinationCountry))]
    public int? DestinationCountryId { get; set; }
    public Area? DestinationCountry { get; set; }

    [ForeignKey(nameof(DestinationProvince))]
    public int? DestinationProvinceId { get; set; }
    public Area? DestinationProvince { get; set; }

    [ForeignKey(nameof(DestinationCity))]
    public int? DestinationCityId { get; set; }
    public Area? DestinationCity { get; set; }

    [ForeignKey(nameof(ManifestFormPeriod))]
    public int? ManifestFormPeriodId { get; set; }
    public ManifestFormPeriod? ManifestFormPeriod { get; set; }

    public string? Date { get; set; }

    public string? CompanySenderDescription { get; set; }

    public string? CompanySenderDescriptionForPrint { get; set; }

    public string? CompanyReceiverDescription { get; set; }

    public string? MasterWaybillNo { get; set; }

    public decimal? MasterWaybillWeight { get; set; }

    public string? MasterWaybillAirline { get; set; }

    public string? MasterWaybillFlightNo { get; set; }

    public string? MasterWaybillFlightDate { get; set; }

    public short? State { get; set; }
    public WaybillState? CompanyManifestSate { get; set; }

    public string? DateIssued { get; set; }

    public string? TimeIssued { get; set; }

    public string? DateAirlineDelivery { get; set; }

    public string? TimeAirlineDelivery { get; set; }

    public string? DateReceivedAtReceiverCompany { get; set; }

    public string? TimeReceivedAtReceiverCompany { get; set; }

    [ForeignKey(nameof(Counter))]
    public string? CounterId { get; set; }
    public User? Counter { get; set; }

    public bool? Dirty { get; set; }
}