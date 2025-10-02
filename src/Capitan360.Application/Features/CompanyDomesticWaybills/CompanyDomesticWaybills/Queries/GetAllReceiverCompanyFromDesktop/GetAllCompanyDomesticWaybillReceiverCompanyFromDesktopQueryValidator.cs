using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Dtos;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Queries.GetAllReceiverCompany;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Queries.GetAllReceiverCompanyFromDesktop;

public class GetAllCompanyDomesticWaybillReceiverCompanyFromDesktopQueryValidator : AbstractValidator<GetAllCompanyDomesticWaybillReceiverCompanyFromDesktopQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(CompanyDomesticWaybillDto.No),
        nameof(CompanyDomesticWaybillDto.CompanySenderName),
        nameof(CompanyDomesticWaybillDto.SourceCityName),
        nameof(CompanyDomesticWaybillDto.CompanyManifestFormNo),
        nameof(CompanyDomesticWaybillDto.GrossWeight),
        nameof(CompanyDomesticWaybillDto.ChargeableWeight),
        nameof(CompanyDomesticWaybillDto.ExitTotalCost),
        nameof(CompanyDomesticWaybillDto.CompanyReceiverDateFinancial),
        nameof(CompanyDomesticWaybillDto.Dirty)];

    public GetAllCompanyDomesticWaybillReceiverCompanyFromDesktopQueryValidator()
    {
        RuleFor(x => x.No)
            .GreaterThanOrEqualTo(0).WithMessage("شماره بارنامه باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.CompanyReceiverCaptain360Code)
            .NotNull().WithMessage("کد کاپیتان 360 شرکت گیرنده  نمی‌تواند خالی باشد")
            .NotEmpty().WithMessage("کد کاپیتان 360 شرکت گیرنده الزامی است")
            .MaximumLength(10).WithMessage("کد کاپیتان 360 شرکت گیرنده نمی‌تواند بیشتر از 10 کاراکتر باشد");

        RuleFor(x => x.CompanyManifestFormNo)
            .GreaterThanOrEqualTo(0).WithMessage("شماره فرم مانیفست باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.PaymentType)
            .GreaterThanOrEqualTo(0).WithMessage("نوع پرداخت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.BankId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه بانک باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => (int)x.State)
            .GreaterThanOrEqualTo(0).WithMessage("وضعیت بارنامه باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.BikeId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه پیک باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.PaymentType)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه وضعیت بارنامه باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.Lock)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت قابل ویرایش بودن بارنامه می تواند یکی از حالت قابل ویرایش، غیر قابل ویرایش و یا همه باشد.");

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
