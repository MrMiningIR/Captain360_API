using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.Addresses.Areas.Queries.GetMunicipalArea;

public record GetMunicipalAreaQuery(
     [property: DefaultValue(0)] int ParentId = 0,
    [property: DefaultValue("")] string? SearchPhrase = "",
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending,
    [property: DefaultValue(false)] bool IgnorePageSize = false
);