namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Dtos;

public class TableDataDto
{
    public int MunicipalAreaId { get; set; }
    public string MunicipalAreaPesrsianName { get; set; }

    public Dictionary<int, FieldDataDto> Fields { get; set; } = new();
    public int CompanyDomesticPathId { get; set; }
    public int PathStructType { get; set; }
    public int Id { get; set; }
}