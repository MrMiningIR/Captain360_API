using System.ComponentModel;
using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Companies.CompanyCommissionses.Queries.GetAll;

public record GetAllCompanyCommissionsQuery(
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(0)] int CompanyTypeId = 0,
    [property: DefaultValue(0)] int CompanyId = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);