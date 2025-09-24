using System.ComponentModel;
using Capitan360.Application.Features.Companies.CompanyPreferenceses.Dtos;
using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyPreferenceses.Queries.GetAll;

public class GetAllCompanyPreferencesQueryValidator : AbstractValidator<GetAllCompanyPreferencesQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(CompanyPreferencesDto.CompanyName),
        nameof(CompanyPreferencesDto.CaptainCargoName),
        nameof(CompanyPreferencesDto.CaptainCargoCode)
    ];

    public GetAllCompanyPreferencesQueryValidator()
    {
        RuleFor(x => x.CompanyTypeId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه نوع شرکت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.CompanyId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه شرکت باید بزرگتر یا مساوی صفر باشد");

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