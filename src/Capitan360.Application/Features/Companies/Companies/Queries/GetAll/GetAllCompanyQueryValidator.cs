using Capitan360.Application.Features.Companies.Companies.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.Companies.Queries.GetAll;

public class GetAllCompanyQueryValidator : AbstractValidator<GetAllCompanyQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(CompanyDto.Code),
        nameof(CompanyDto.Name),
        nameof(CompanyDto.CompanyTypeName)];
    public GetAllCompanyQueryValidator()
    {
        RuleFor(x => x.CompanyTypeId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("شناسه نوع شرکت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.CityId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("شناسه شهر باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.CompanyTypeId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("شناسه نوع شرکت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("شماره صفحه باید بزرگتر یا مساوی یک باشد");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}