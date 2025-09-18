using Capitan360.Application.Features.Identities.Identities.Dtos;
using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.UserCompany.Queries.GetUsersByCompany;

public class GetUsersByCompanyQueryValidator : AbstractValidator<GetUsersByCompanyQuery>
{
    private readonly int[] _allowPageSizes = [5, 10, 15, 30];
    private readonly int[] _allowUserKind = [(int)UserKind.Normal, (int)UserKind.Special, (int)UserKind.All];
    private readonly string[] _allowedSortByColumnNames = [
        nameof(UserDto.FullName),
        nameof(UserDto.LastAccess),
        nameof(UserDto.PhoneNumber)
    ];
    public GetUsersByCompanyQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه شرکت باید بزرگ‌تر یا برابر صفر باشد");


        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"اندازه صفحه باید یکی از [{string.Join(",", _allowPageSizes)}] باشد");



        //RuleFor(r => r.UserKind)
        //    .Must(value => Enum.IsDefined(typeof(UserKind), value)) // شرط درست
        //    .WithMessage($"تایپ کاربر باید از این مقادیر باشد: {string.Join(", ", Enum.GetValues(typeof(UserKind)))}");
        //       RuleFor(r => r.UserKind)
        //.Must(value => Enum.IsDefined(typeof(UserKind), value)) // بررسی مقدار عددی
        //.WithMessage($"تایپ کاربر باید از این مقادیر باشد: {string.Join(", ", Enum.GetValues(typeof(UserKind)).Cast<int>())}");

        RuleFor(r => r.UserKind)
            .Must(value => _allowUserKind.Contains(value))
            .WithMessage($"تایپ کاربر باید از این مقادیر باشد [{string.Join(",", _allowUserKind)}] باشد");



        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"مرتب‌سازی اختیاری است یا باید یکی از [{string.Join(",", _allowedSortByColumnNames)}] باشد");
    }

}