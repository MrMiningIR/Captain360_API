using Capitan360.Application.Features.ContentTypes.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.ContentTypes.Queries.GetAll;

public class GetAllContentTypesQueryValidator : AbstractValidator<GetAllContentTypesQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(ContentTypeDto.Order)
    ];

    public GetAllContentTypesQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("شماره صفحه باید بزرگتر یا مساوی یک باشد");

        RuleFor(r => r.CompanyTypeId)
            .GreaterThan(0)
            .WithMessage("شناسه نوع شرکت معتبر نیست.");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}