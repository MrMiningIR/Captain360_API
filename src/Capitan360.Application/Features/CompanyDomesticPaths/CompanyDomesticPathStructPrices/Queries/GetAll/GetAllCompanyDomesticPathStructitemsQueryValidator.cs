using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Queries.GetAll;

public class GetAllCompanyDomesticPathStructitemsQueryValidator : AbstractValidator<GetAllCompanyDomesticPathStructQuery>
{
    private readonly int[] _allowPageSizes = { 5, 10, 15, 30 };
    private readonly string[] _allowedSortByColumnNames = {
        nameof(CompanyDomesticPathStructPriceDto.Weight),
        nameof(CompanyDomesticPathStructPriceDto.PathStructType),
        nameof(CompanyDomesticPathStructPriceDto.WeightType)
    };

    public GetAllCompanyDomesticPathStructitemsQueryValidator()
    {
        RuleFor(r => r.CompanyDomesticPathId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("شناسه مسیر داخلی شرکت معتبر نیست.");

        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"اندازه صفحه باید در [{string.Join(",", _allowPageSizes)}] باشد");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"مرتب‌سازی اختیاری است یا باید در [{string.Join(",", _allowedSortByColumnNames)}] باشد");
    }
}