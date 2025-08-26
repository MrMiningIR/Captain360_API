using Capitan360.Application.Services.ContentTypeService.Dtos;
using FluentValidation;

namespace Capitan360.Application.Services.ContentTypeService.Queries.GetAllContentTypes;

public class GetAllContentTypesQueryValidator : AbstractValidator<GetAllContentTypesQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(ContentTypeDto.ContentTypeOrder)
    ];

    public GetAllContentTypesQueryValidator()
    {


        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);
        RuleFor(r => r.CompanyTypeId)
            .GreaterThanOrEqualTo(0);

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}