namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Dtos;

public class FieldDataDto
{
    public int? Weight { get; set; } // برای بدون نام
    public long? Price { get; set; } // برای مناطق
    public bool Static { get; set; }
    public int Id { get; set; }
    public int CompanyDomesticPathStructPriceId { get; set; }
}