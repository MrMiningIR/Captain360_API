using Capitan360.Domain.Constants;
using Capitan360.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capitan360.Application.Features.Companies.CompanyUris.Queries.GetAll;

public record GetAllCompanyUrisQuery(
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(0)] int CompanyId = 0,
    [property: DefaultValue(1)] int Active = 1,
    [property: DefaultValue(1)] int Captain360Uri = 1,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);