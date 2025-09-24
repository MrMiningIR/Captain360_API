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
        RuleFor(x => x.CompanyId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه شرکت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.CompanyTypeId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه نوع شرکت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.CityId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه شهر باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.IsParentCompany)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت شرکت می تواند یکی از حالت شرکت اصلی، شرکت زیر مجموعه و یا همه باشد.");

        RuleFor(x => x.Active)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت فعالیت شرکت می تواند یکی از حالت فعال، غیر فعال و یا همه باشد.");

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