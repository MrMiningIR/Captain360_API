using Capitan360.Application.Features.Companies.CompanyTypes.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyTypes.Queries.GetAll;

public class GetAllCompanyTypesQueryValidator : AbstractValidator<GetAllCompanyTypesQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(CompanyTypeDto.TypeName),
        nameof(CompanyTypeDto.DisplayName)
    ];

    public GetAllCompanyTypesQueryValidator()
    {
        RuleFor(r => r.PageNumber)
             .GreaterThanOrEqualTo(1).WithMessage("شماره صفحه باید بزرگتر یا مساوی یک باشد");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"تعداد ایتم در صفحه باید یکی از موارد زیر باشد [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"مرتب سازی باید بر اساس یکی از آیتم های زیر باشد [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}