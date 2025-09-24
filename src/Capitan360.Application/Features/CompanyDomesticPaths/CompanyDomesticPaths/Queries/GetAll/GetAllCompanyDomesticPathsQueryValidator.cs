using System.ComponentModel;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Dtos;
using Capitan360.Domain.Enums;
using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Queries.GetAll;

public class GetAllCompanyDomesticPathsQueryValidator : AbstractValidator<GetAllCompanyDomesticPathsQuery>
{
    private readonly int[] _allowPageSizes = { 5, 10, 15, 30 };
    private readonly string[] _allowedSortByColumnNames = {
        nameof(CompanyDomesticPathDto.SourceCityName),
        nameof(CompanyDomesticPathDto.DestinationCityName)
    };

    public GetAllCompanyDomesticPathsQueryValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه شرکت باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.SourceCityId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه شهر مبدا باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.DestinationCityId)
            .GreaterThanOrEqualTo(0).WithMessage("شناسه شهر مقصد باید بزرگتر یا مساوی صفر باشد");

        RuleFor(x => x.Active)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت فعالیت مسیر می تواند یکی از حالت فعال، غیر فعال و یا همه باشد.");

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