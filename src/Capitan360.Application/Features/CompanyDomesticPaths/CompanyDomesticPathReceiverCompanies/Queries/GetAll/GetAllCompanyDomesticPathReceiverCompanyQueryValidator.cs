using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathReceiverCompanies.Queries.GetAll;

public class GetAllCompanyDomesticPathReceiverCompanyQueryValidator : AbstractValidator<GetAllCompanyDomesticPathReceiverCompanyQuery>
{
    private readonly int[] _allowPageSizes = { 5, 10, 15, 30 };
    private readonly string[] _allowedSortByColumnNames = {
    nameof(CompanyDomesticPathReceiverCompanyDto.MunicipalAreaName)
};

    public GetAllCompanyDomesticPathReceiverCompanyQueryValidator()
    {
        RuleFor(x => x.CompanyPathId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("شناسه شرکت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("شماره صفحه باید بزرگتر یا مساوی یک باشد");

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"تعداد ایتم در صفحه باید یکی از موارد زیر باشد [{string.Join(",", _allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"مرتب سازی باید بر اساس یکی از آیتم های زیر باشد [{string.Join(",", _allowedSortByColumnNames)}]");
    }
}
