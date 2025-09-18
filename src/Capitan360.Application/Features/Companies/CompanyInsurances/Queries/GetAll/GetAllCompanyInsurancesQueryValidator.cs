using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capitan360.Application.Features.Companies.CompanyInsurances.Dtos;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Dtos;
using Capitan360.Application.Features.Companies.CompanyPackageTypes.Queries.GetAll;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyInsurances.Queries.GetAll;

internal class GetAllCompanyInsurancesQueryValidator : AbstractValidator<GetAllCompanyInsurancesQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];

    private string[] _allowedSortByColumnNames = [
        nameof(CompanyInsuranceDto.Code)
    ];

    public GetAllCompanyInsurancesQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("شماره صفحه باید بزرگتر یا مساوی یک باشد");

        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}
