using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.Addresses.Addresses.Queries.GetAll;

public record GetAllAddressQuery(
    [property: DefaultValue(0)] int CompanyId = 0,
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(1)] int Active = 1,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending);