using System.ComponentModel;
using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Queries.GetAll;

public record GetAllCompanyDomesticPathReceiverCompanyQuery(
[property: DefaultValue(0)] int CompanyPathId = 0,
[property: DefaultValue("")] string SearchPhrase = "",
[property: DefaultValue(null)] string? SortBy = null,
[property: DefaultValue(1)] int PageNumber = 1,
[property: DefaultValue(15)] int PageSize = 15,
[property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);
