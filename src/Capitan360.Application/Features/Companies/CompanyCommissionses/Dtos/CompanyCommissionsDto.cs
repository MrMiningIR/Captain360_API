namespace Capitan360.Application.Features.Companies.CompanyCommissionses.Dtos;

public class CompanyCommissionsDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public long CommissionFromCaptainCargoWebSite { get; set; }
    public long CommissionFromCompanyWebSite { get; set; }
    public long CommissionFromCaptainCargoWebService { get; set; }
    public long CommissionFromCompanyWebService { get; set; }
    public long CommissionFromCaptainCargoPanel { get; set; }
    public long CommissionFromCaptainCargoDesktop { get; set; }
}