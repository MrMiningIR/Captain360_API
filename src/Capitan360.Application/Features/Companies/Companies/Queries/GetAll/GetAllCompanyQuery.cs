using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.Companies.Companies.Queries.GetAll;

public record GetAllCompanyQuery(
    [property: DefaultValue("")] string SearchPhrase = "",
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(0)] int CompanyId = 0,
    [property: DefaultValue(0)] int CompanyTypeId = 0,
    [property: DefaultValue(0)] int CityId = 0,
    [property: DefaultValue(0)] int IsParentCompany = 0,
    [property: DefaultValue(0)] int Active = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);



