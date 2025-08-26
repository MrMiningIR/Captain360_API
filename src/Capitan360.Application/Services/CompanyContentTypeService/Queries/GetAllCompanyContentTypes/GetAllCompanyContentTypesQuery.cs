using Capitan360.Domain.Constants;
using System.ComponentModel;

namespace Capitan360.Application.Services.CompanyContentTypeService.Queries.GetAllCompanyContentTypes;

public record GetAllCompanyContentTypesQuery(
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(0)] int CompanyId = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(15)] int PageSize = 15,
    [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending);