using FluentValidation;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPaths.Commands.Create;

public class CreateCompanyDomesticPathCommandValidator : AbstractValidator<CreateCompanyDomesticPathCommand>
{
    public CreateCompanyDomesticPathCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("شناسه شرکت الزامی است");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("توضیحات نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("حداکثر طول توضیحات 500 کاراکتر است.");

        RuleFor(x => x.DescriptionForSearch)
            .NotNull().WithMessage("توضیحات مسیر نمی تواند خالی باشد است.")
            .MaximumLength(500).WithMessage("حداکثر طول توضیحات مسیر 500 کاراکتر است.");

        RuleFor(x => x.SourceCountryId)
            .GreaterThan(0).WithMessage("شناسه کشور مبدا الزامی است");

        RuleFor(x => x.SourceProvinceId)
            .GreaterThan(0).WithMessage("شناسه استان مبدا الزامی است");

        RuleFor(x => x.SourceCityId)
            .GreaterThan(0).WithMessage("شناسه شهر مبدا الزامی است");

        RuleFor(x => x.DestinationCountryId)
            .GreaterThan(0).WithMessage("شناسه کشور مقصد الزامی است");

        RuleFor(x => x.DestinationProvinceId)
            .GreaterThan(0).WithMessage("شناسه استان مقصد الزامی است");

        RuleFor(x => x.DestinationCityId)
            .GreaterThan(0).WithMessage("شناسه شهر مقصد الزامی است");
    }
}