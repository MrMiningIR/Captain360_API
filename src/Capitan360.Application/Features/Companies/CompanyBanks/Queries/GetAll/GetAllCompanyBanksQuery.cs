using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.Companies.CompanyBanks.Queries.GetAll;

public record GetAllCompanyBanksQuery(
    [property: DefaultValue("")] string SearchPhrase = "",
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(0)] int CompanyId = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(15)] int PageSize = 15,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);
