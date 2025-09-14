using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Services.UserCompany.Queries.GetUsersByCompany;

public record GetUsersByCompanyQuery(

    [property: DefaultValue((int)UserKind.Normal)] int UserKind = 2,
    [property: DefaultValue(0)] int CompanyType = 0,
    [property: DefaultValue(null)] string? SearchPhrase = null,
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Descending)] SortDirection SortDirection = SortDirection.Descending

)
{
    public int CompanyId { get; set; } = 0;
};