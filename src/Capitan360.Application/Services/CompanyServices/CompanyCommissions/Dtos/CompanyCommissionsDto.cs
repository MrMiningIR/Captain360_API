namespace Capitan360.Application.Services.CompanyServices.CompanyCommissions.Dtos;

public class CompanyCommissionsDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public decimal CommissionFromCaptainCargoWebSite { get; set; }
    public decimal CommissionFromCompanyWebSite { get; set; }
    public decimal CommissionFromCaptainCargoWebService { get; set; }
    public decimal CommissionFromCompanyWebService { get; set; }
    public decimal CommissionFromCaptainCargoPanel { get; set; }
    public decimal CommissionFromCaptainCargoDesktop { get; set; }
}