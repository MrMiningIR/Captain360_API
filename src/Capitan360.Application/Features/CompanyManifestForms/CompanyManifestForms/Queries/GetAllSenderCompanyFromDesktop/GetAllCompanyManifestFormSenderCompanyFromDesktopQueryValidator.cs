using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Queries.GetAllSenderCompanyFromDesktop;

public class GetAllCompanyManifestFormSenderCompanyFromDesktopQueryValidator : AbstractValidator<GetAllCompanyManifestFormSenderCompanyFromDesktopQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(CompanyManifestFormDto.No),
        nameof(CompanyManifestFormDto.CompanyReceiverName),
        nameof(CompanyManifestFormDto.CompanyReceiverUserInsertedCode),
        nameof(CompanyManifestFormDto.DestinationCityName),
        nameof(CompanyManifestFormDto.Dirty)];

    public GetAllCompanyManifestFormSenderCompanyFromDesktopQueryValidator()
    {
        RuleFor(x => x.No)
            .GreaterThanOrEqualTo(0).WithMessage("بارنامه باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.CompanySenderCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت گیرنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت گیرنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت گیرنده نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => (int)x.State)
            .GreaterThanOrEqualTo(0).WithMessage("وضعیت بارنامه باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.Dirty)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت ویرایش بارنامه می تواند یکی از حالت ویرایش شده، ویرایش نشده و یا همه باشد.");

        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("شماره صفحه باید بزرگتر یا مساوی یک باشد");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"تعداد ایتم در صفحه باید یکی از موارد زیر باشد [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"مرتب سازی باید بر اساس یکی از آیتم های زیر باشد [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}
