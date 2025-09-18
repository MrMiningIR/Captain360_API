using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.Companies.CompanyUri.Queries.GetAllCompanyUris;

public record GetAllCompanyUrisQuery(

    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,

    [property: DefaultValue(1)] int Active = 1,
    [property: DefaultValue(1)] int Captain360Uri = 1,

    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,

    [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending);