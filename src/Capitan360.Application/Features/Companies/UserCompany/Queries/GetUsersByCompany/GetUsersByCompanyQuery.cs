using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.Companies.UserCompany.Queries.GetUsersByCompany;

public record GetUsersQuery(

    [property: DefaultValue(0)] int CompanyType = 0,
    [property: DefaultValue(1)] int CompanyId = 0,
    [property: DefaultValue(0)] int ModaianFactorTypeId = 0,
    [property: DefaultValue(0)] int HasCredit = 0,
    [property: DefaultValue(0)] int Banned = 0,
    [property: DefaultValue(0)] int Active = 0,
    [property: DefaultValue(0)] int IsBike = 0,

    [property: DefaultValue("")] string RoleId = "",
    [property: DefaultValue("")] string? SearchPhrase = "",
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)]
    SortDirection SortDirection = SortDirection.Descending

)
{

    public int CompanyId { get; set; } = CompanyId;
};

