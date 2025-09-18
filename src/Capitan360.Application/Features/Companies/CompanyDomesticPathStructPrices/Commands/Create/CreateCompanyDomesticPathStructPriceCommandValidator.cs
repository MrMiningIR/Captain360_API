using FluentValidation;

namespace Capitan360.Application.Features.Companies.CompanyDomesticPathStructPrices.Commands.Create;

//public class CreateCompanyDomesticPathStructPriceValidator : AbstractValidator<CreateCompanyDomesticPathListStructPriceCommand>
//{
//    public CreateCompanyDomesticPathStructPriceValidator()
//    {
//        RuleFor(x => x.items)
//            .NotEmpty().WithMessage("لیست قیمت‌های ساختار مسیر نمی‌تواند خالی باشد");

//        RuleForEach(x => x.items).ChildRules(item =>
//        {
//            item.RuleFor(p => p.CompanyDomesticPathId)
//                .GreaterThan(0).WithMessage("شناسه مسیر داخلی شرکت الزامی است");

//            item.RuleFor(p => p.Weight)
//                .GreaterThan(0).WithMessage("وزن باید بزرگ‌تر از صفر باشد");

//            item.RuleFor(p => p.PathStructType)
//                .IsInEnum().WithMessage("نوع ساختار مسیر نامعتبر است");

//            item.RuleFor(p => p.WeightType)
//                .IsInEnum().WithMessage("نوع وزن نامعتبر است");
//        });
//    }
//}