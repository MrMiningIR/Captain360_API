using Capitan360.Domain.Enums;
using System.ComponentModel;

namespace Capitan360.Application.Features.Identities.Users.Users.Queries.GetAll;

public record GetAllUserQuery(
    [property: DefaultValue("")] string SearchPhrase = "",
    [property: DefaultValue(null)] string? SortBy = null,
    [property: DefaultValue(0)] int CompanyId = 0,
    [property: DefaultValue(0)] int CompanyTypeId = 0,
    [property: DefaultValue(0)] int RoleId = 0,
    [property: DefaultValue(1)] int TypeOfFactorInSamanehMoadianId = -1,
    [property: DefaultValue(0)] int HasCredit = 0,
    [property: DefaultValue(0)] int Baned = 0,
    [property: DefaultValue(0)] int Active = 0,
    [property: DefaultValue(1)] int PageNumber = 1,
    [property: DefaultValue(10)] int PageSize = 10,
    [property: DefaultValue(SortDirection.Ascending)] SortDirection SortDirection = SortDirection.Ascending);