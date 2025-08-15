using Capitan360.Application.Services.CompanyContentTypeService.Dtos;
using FluentValidation;

namespace Capitan360.Application.Services.CompanyContentTypeService.Queries.GetAllCompanyContentTypes;

public class GetAllCompanyContentTypesQueryValidator : AbstractValidator<GetAllCompanyContentTypesQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];

    private string[] _allowedSortByColumnNames = [
        nameof(CompanyContentTypeDto.ContentTypeName),
        nameof(CompanyContentTypeDto.CompanyContentTypeActive),
        nameof(CompanyContentTypeDto.CompanyContentTypeOrder)
    ];

    public GetAllCompanyContentTypesQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);
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