using Capitan360.Domain.Constants;
using System.ComponentModel;

namespace Capitan360.Application.Services.CompanyServices.CompanyUri.Queries.GetAllCompanyUris;

public record GetAllCompanyUrisQuery(
    [property: DefaultValue(0)] int CompanyId,
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(1)] int Active = 1,
    [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending);