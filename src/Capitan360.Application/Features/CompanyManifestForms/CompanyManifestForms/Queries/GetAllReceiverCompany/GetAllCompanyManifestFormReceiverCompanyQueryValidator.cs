using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Dtos;
using Capitan360.Application.Features.CompanyDomesticWaybills.CompanyDomesticWaybills.Queries.GetAllReceiverCompany;
using Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Dtos;
using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyManifestForms.CompanyManifestForms.Queries.GetAllReceiverCompany;

public class GetAllCompanyManifestFormReceiverCompanyQueryValidator : AbstractValidator<GetAllCompanyManifestFormReceiverCompanyQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(CompanyManifestFormDto.No),
        nameof(CompanyManifestFormDto.CompanySenderName),
        nameof(CompanyManifestFormDto.SourceCityName),
        nameof(CompanyManifestFormDto.Dirty)];

    public GetAllCompanyManifestFormReceiverCompanyQueryValidator()
    {
        RuleFor(x => x.No)
            .GreaterThanOrEqualTo(0).WithMessage("شماره فرم مانیفست باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.CompanySenderId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه شرکت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.SourceCityId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه شهر باید بزرگتر یا مساوی صفر باشد");

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
