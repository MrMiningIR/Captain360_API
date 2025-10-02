namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Dtos;

public class CompanyDomesticPathReceiverCompanyDto
{
    public int Id { get; set; }
    public int CompanyDomesticPathId { get; set; }
    public string? CompanyDomesticPathDestinationCityName { get; set; }
    public int MunicipalAreaId { get; set; }
    public string? MunicipalAreaName { get; set; }
    public int? ReceiverCompanyId { get; set; }
    public string? ReceiverCompanyName { get; set; }
    public string? ReceiverCompanyUserInsertedCode { get; set; }
    public string? ReceiverCompanyUserInsertedName { get; set; }
    public string? ReceiverCompanyUserInsertedTelephone { get; set; }
    public string? ReceiverCompanyUserInsertedAddress { get; set; }
    public string DescriptionForPrint1 { get; set; } = default!;
    public string DescriptionForPrint2 { get; set; } = default!;
    public string DescriptionForPrint3 { get; set; } = default!;
}
