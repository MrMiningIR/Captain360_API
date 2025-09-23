using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Commands.Update;

public record UpdateCompanyBankCommand(
    string Code,
    string Name,
    string Description,
    bool Active)
{
    public int Id { get; set; }
};
