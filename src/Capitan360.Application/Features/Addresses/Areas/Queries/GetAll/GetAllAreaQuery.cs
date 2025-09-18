using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.Addresses.Areas.Queries.GetAll;

public record GetAllAreaQuery(
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending,
    [property: DefaultValue(false)] bool IgnorePageSize = false,
     [property: DefaultValue(-1)] int ProvinceId = -1
    );