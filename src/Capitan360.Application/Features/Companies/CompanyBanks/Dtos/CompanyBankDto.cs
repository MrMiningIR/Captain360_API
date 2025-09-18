using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capitan360.Domain.Entities.Companies;
using Capitan360.Domain.Entities.DomesticWaybills;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Dtos;

public class CompanyBankDto
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? CompanyName { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool Active { get; set; }
    public int Order { get; set; }
}