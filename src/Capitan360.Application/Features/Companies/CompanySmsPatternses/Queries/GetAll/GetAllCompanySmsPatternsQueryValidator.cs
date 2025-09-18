using Capitan360.Application.Features.Companies.CompanySmsPatterns.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanySmsPatterns.Queries.GetAllCompanySmsPatterns;

public class GetAllCompanySmsPatternsQueryValidator : AbstractValidator<GetAllCompanySmsPatternsQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(CompanySmsPatternsDto.CompanyName),
        nameof(CompanySmsPatternsDto.SmsPanelUserName),
        nameof(CompanySmsPatternsDto.SmsPanelNumber),
    ];

    public GetAllCompanySmsPatternsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}