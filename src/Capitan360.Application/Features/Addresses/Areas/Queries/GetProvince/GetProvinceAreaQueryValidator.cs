using Capitan360.Application.Features.Addresses.Areas.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Areas.Queries.GetProvince;

public class GetProvinceAreaQueryValidator : AbstractValidator<GetProvinceAreaQuery>
{
    private readonly int[] _allowPageSizes = [5, 10, 15, 30];
    private readonly string[] _allowedSortByColumnNames = [
        nameof(AreaDto.PersianName),
        nameof(AreaDto.Code),
        nameof(AreaDto.LevelId)
    ];

    public GetProvinceAreaQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("شماره صفحه باید بزرگتر یا برابر با 1 باشد");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value)).
            When(x => !x.IgnorePageSize)
            .WithMessage($"اندازه صفحه باید یکی از [{string.Join(",", _allowPageSizes)}] باشد");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"مرتب‌سازی اختیاری است یا باید یکی از [{string.Join(",", _allowedSortByColumnNames)}] باشد");
    }
}