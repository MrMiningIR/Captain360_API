using System.ComponentModel.DataAnnotations.Schema;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.BaseEntities;
using Capitan360.Domain.Entities.Companies;

namespace Capitan360.Domain.Entities.CompanyDomesticPaths;

public class CompanyDomesticPathReceiverCompany : BaseEntity
{
    [ForeignKey(nameof(CompanyDomesticPath))]
    public int CompanyDomesticPathId { get; set; }
    public CompanyDomesticPath? CompanyDomesticPath { get; set; }

    [ForeignKey(nameof(MunicipalArea))]
    public int MunicipalAreaId { get; set; }
    public Area? MunicipalArea { get; set; }

    [ForeignKey(nameof(ReceiverCompany))]
    public int? ReceiverCompanyId { get; set; }
    public Company? ReceiverCompany { get; set; }
    public string? ReceiverCompanyUserInsertedCode { get; set; }
    public string? ReceiverCompanyUserInsertedName { get; set; }
    public string? ReceiverCompanyUserInsertedTelephone { get; set; }
    public string? ReceiverCompanyUserInsertedAddress { get; set; }

    public string DescriptionForPrint1 { get; set; } = default!;

    public string DescriptionForPrint2 { get; set; } = default!;

    public string DescriptionForPrint3 { get; set; } = default!;
}
