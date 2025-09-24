using Capitan360.Application.Features.Addresses.Areas.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.Addresses.Areas.Queries.GetCity;

public class GetCityAreaQueryValidator : AbstractValidator<GetCityAreaQuery>
{
    private readonly int[] _allowPageSizes = [5, 10, 15, 30];
    private readonly string[] _allowedSortByColumnNames = [
        nameof(AreaDto.PersianName),
        nameof(AreaDto.Code),
        nameof(AreaDto.LevelId)
    ];

    public GetCityAreaQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("شماره صفحه باید بزرگتر یا برابر با 1 باشد");
        RuleFor(r => r.ProvinceId)
            .GreaterThan(0).WithMessage("شماسه استان باید بیشتر از 0 باشد");

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