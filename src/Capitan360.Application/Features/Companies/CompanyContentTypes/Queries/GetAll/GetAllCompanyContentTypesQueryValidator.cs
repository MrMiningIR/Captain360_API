using Capitan360.Application.Features.Companies.CompanyContentTypes.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyContentTypes.Queries.GetAll;

public class GetAllCompanyContentTypesQueryValidator : AbstractValidator<GetAllCompanyContentTypesQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];

    private string[] _allowedSortByColumnNames = [
        nameof(CompanyContentTypeDto.Order)
    ];

    public GetAllCompanyContentTypesQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("شماره صفحه باید بزرگتر یا مساوی یک باشد");

        RuleFor(x => x.CompanyId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("شناسه شرکت الزامی است");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}