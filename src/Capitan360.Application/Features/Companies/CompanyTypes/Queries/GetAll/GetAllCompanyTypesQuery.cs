using System.ComponentModel;
using Capitan360.Domain.Enums;

namespace Capitan360.Application.Features.Companies.CompanyTypes.Queries.GetAll;

public record GetAllCompanyTypesQuery(
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending);