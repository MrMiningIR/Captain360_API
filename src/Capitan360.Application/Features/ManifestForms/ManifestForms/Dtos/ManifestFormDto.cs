using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.ManifestForms.ManifestForms.Dtos;

public class ManifestFormDto 
{
    public int Id { get; set; }
    public long No { get; set; }
    public int CompanySenderId { get; set; }
    public string? CompanySenderName { get; set; }
    public int? CompanyReceiverId { get; set; }
    public string? CompanyReceiverName { get; set; }
    public string? CompanyReceiverUserInsertedCode { get; set; }
    public string? CompanyReceiverUserInsertedName { get; set; }
    public int? SourceCountryId { get; set; }
    public string? SourceCountryName { get; set; }
    public int? SourceProvinceId { get; set; }
    public string? SourceProvinceName { get; set; }
    public int? SourceCityId { get; set; }
    public string? SourceCityName { get; set; }
    public int? DestinationCountryId { get; set; }
    public string? DestinationCountryName { get; set; }
    public int? DestinationProvinceId { get; set; }
    public string? DestinationProvinceName { get; set; }
    public int? DestinationCityId { get; set; }
    public string? DestinationCityName { get; set; }
    public int? ManifestFormPeriodId { get; set; }
    public string? ManifestFormPeriodCode { get; set; }
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
    public ManifestFormState? CompanyManifestSate { get; set; }
    public string? DateIssued { get; set; }
    public string? TimeIssued { get; set; }
    public string? DateAirlineDelivery { get; set; }
    public string? TimeAirlineDelivery { get; set; }
    public string? DateReceivedAtReceiverCompany { get; set; }
    public string? TimeReceivedAtReceiverCompany { get; set; }
    public string? CounterId { get; set; }
    public string? CounterNameFamily { get; set; }
    public bool? Dirty { get; set; }
}
