using Capitan360.Application.Features.Companies.CompanyUris.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyUris.Queries.GetAll;

public class GetAllCompanyUrisQueryValidator : AbstractValidator<GetAllCompanyUrisQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];
    private string[] _allowedSortByColumnNames = [
        nameof(CompanyUriDto.Uri),
        nameof(CompanyUriDto.CompanyName),
        nameof(CompanyUriDto.Active),
        nameof(CompanyUriDto.Captain360Uri)
    ];

    public GetAllCompanyUrisQueryValidator()
    {
        RuleFor(r => r.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت معتبر نیست.");

        RuleFor(x => x.Captain360Uri)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت آدرس URI اصلی شرکت می تواند یکی از حالت شرکت اصلی، فرعی و یا همه باشد.");

        RuleFor(x => x.Active)
            .InclusiveBetween(-1, 1).WithMessage("وضعیت فعالیت URI شرکت می تواند یکی از حالت فعال، غیر فعال و یا همه باشد.");

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