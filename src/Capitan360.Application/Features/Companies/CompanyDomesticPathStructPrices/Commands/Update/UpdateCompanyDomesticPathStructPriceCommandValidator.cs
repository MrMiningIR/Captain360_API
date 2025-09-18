using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Update;

//public class UpdateCompanyDomesticPathStructPriceValidator : AbstractValidator<UpdateCompanyDomesticPathStructPriceCommand>
//{
//    public UpdateCompanyDomesticPathStructPriceValidator()
//    {
//        RuleFor(x => x.CompanyDomesticPathId)
//            .GreaterThan(0).WithMessage("شناسه مسیر داخلی شرکت باید بزرگ‌تر از صفر باشد");

//        RuleFor(x => x.Weight)
//            .GreaterThan(0).WithMessage("وزن باید بزرگ‌تر از صفر باشد");

//        RuleFor(x => x.PathStructType)
//            .IsInEnum().WithMessage("نوع ساختار مسیر نامعتبر است");

//        RuleFor(x => x.WeightType)
//            .IsInEnum().WithMessage("نوع وزن نامعتبر است");
//    }
//}