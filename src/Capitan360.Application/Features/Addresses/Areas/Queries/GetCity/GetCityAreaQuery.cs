using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.Addresses.Areas.Queries.GetCity;

public record GetCityAreaQuery(
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending,
    [property: DefaultValue(false)] bool IgnorePageSize = false,
    [property: DefaultValue(0)] int ProvinceId = 0
);