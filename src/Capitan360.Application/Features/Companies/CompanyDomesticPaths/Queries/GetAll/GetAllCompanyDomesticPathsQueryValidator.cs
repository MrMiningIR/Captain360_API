using Capitan360.Application.Features.Companies.CompanyDomesticPaths.Dtos;
using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPaths.Queries.GetAll;

public class GetAllCompanyDomesticPathsQueryValidator : AbstractValidator<GetAllCompanyDomesticPathsQuery>
{
    private readonly int[] _allowPageSizes = { 5, 10, 15, 30 };
    private readonly string[] _allowedSortByColumnNames = {
        nameof(CompanyDomesticPathDto.Description),
        nameof(CompanyDomesticPathDto.Active),
        nameof(CompanyDomesticPathDto.EntranceFee),
        nameof(CompanyDomesticPathDto.EntranceFeeWeight),
        nameof(CompanyDomesticPathDto.EntranceFeeType)
    };

    public GetAllCompanyDomesticPathsQueryValidator()
    {
        RuleFor(r => r.CompanyId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("شناسه شرکت معتبر نیست.");

        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"اندازه صفحه باید در [{string.Join(",", _allowPageSizes)}] باشد");

        RuleFor(r => r.SortBy)
            .Must(value => _allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"مرتب‌سازی اختیاری است یا باید در [{string.Join(",", _allowedSortByColumnNames)}] باشد");


        //RuleFor(r => r.SourceCountryId)
        //    .GreaterThanOrEqualTo(0)
        //    .WithMessage("شناسه کشور معتبر نیست.");
        //RuleFor(r => r.SourceProvinceId)
        //    .GreaterThanOrEqualTo(0)
        //    .WithMessage("شناسه استان معتبر نیست.");
        //RuleFor(r => r.SourceCityId)
        //    .GreaterThanOrEqualTo(0)
        //    .WithMessage("شناسه شهر معتبر نیست.");
        //RuleFor(r => r.DestinationCountryId)
        //    .GreaterThanOrEqualTo(0)
        //    .WithMessage("شناسه کشور معتبر نیست.");
        //RuleFor(r => r.DestinationProvinceId)
        //    .GreaterThanOrEqualTo(0)
        //    .WithMessage("شناسه استان معتبر نیست.");
        RuleFor(r => r.DestinationCityId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("شناسه شهر معتبر نیست.");

    }
}