using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice;
using Capitan360.Domain.Constants;
using FluentValidation;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Services;

public interface ICompanyDomesticPathStructPriceMunicipalAreasService
{
    //Task<ApiResponse<List<int>>> CreateStructPathByList(CreateCompanyDomesticPathStructPriceMunicipalAreasCommand command, CancellationToken cancellationToken);
    //Task<ApiResponse<List<int>>> UpdateCompanyDomesticPathStructPriceAsync(UpdateCompanyDomesticPathListStructPriceCommand command, CancellationToken cancellationToken);
}





//public class CreateCompanyDomesticPathStructPriceMunicipalAreasCommandValidator : AbstractValidator<CreateCompanyDomesticPathStructPriceMunicipalAreasCommand>
//{
//    public CreateCompanyDomesticPathStructPriceMunicipalAreasCommandValidator()
//    {
//        // شرط 1: لیست نباید خالی باشد
//        //RuleFor(command => command.Command)
//        //    .NotEmpty()
//        //    .WithMessage("لیست قیمت‌ها نمی‌تواند خالی باشد.");

//        // شرط 2: حداقل باید 2 وزن وجود داشته باشد
//        RuleFor(command => command.CreateCompanyDomesticPathStructPriceItems)
//            .Must(prices => prices.Count >= 2)
//            .WithMessage("حداقل باید 2 وزن در لیست وجود داشته باشد.");

//        // شرط 3: اولین وزن باید WeightTypeOne باشد
//        RuleFor(command => command.CreateCompanyDomesticPathStructPriceItems)
//            .Must(prices => prices.Any() && prices.First().WeightType == WeightType.WeightTypeOne)
//            .WithMessage("اولین وزن باید از نوع WeightTypeOne باشد.");

//        // شرط 4: دومین وزن باید WeightTypeTwo باشد (اگر حداقل 2 وزن وجود دارد)
//        RuleFor(command => command.CreateCompanyDomesticPathStructPriceItems)
//            .Must(prices => prices.Count >= 2 && prices.ElementAt(1).WeightType == WeightType.WeightTypeTwo)
//            .WithMessage("دومین وزن باید از نوع WeightTypeTwo باشد.");

//        // شرط 5: وزن‌ها باید به ترتیب افزایشی باشند (از کم به زیاد)
//        RuleFor(command => command.CreateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                for (int i = 0; i < prices.Count - 1; i++)
//                {
//                    if (prices[i].Weight >= prices[i + 1].Weight)
//                        return false;
//                }
//                return true;
//            })
//            .WithMessage("وزن‌ها باید به ترتیب افزایشی (از کم به زیاد) باشند.");

//        // شرط 6: ترتیب WeightType باید افزایشی باشد و فاصله‌ای بین آن‌ها وجود نداشته باشد
//        RuleFor(command => command.CreateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var weightTypes = prices.Select(p => (int)p.WeightType).ToList();
//                for (int i = 0; i < weightTypes.Count - 1; i++)
//                {
//                    if (weightTypes[i + 1] != weightTypes[i] + 1)
//                        return false;
//                }
//                return true;
//            })
//            .WithMessage("ترتیب WeightType باید افزایشی باشد و هیچ فاصله‌ای بین آن‌ها وجود نداشته باشد.");

//        // شرط 7: بررسی اینکه فیلدهای CompanyDomesticPathId، Weight، PathStructType و WeightType نباید صفر یا منفی باشند
//        RuleForEach(command => command.CreateCompanyDomesticPathStructPriceItems)
//            .Must(item => item.CompanyDomesticPathId > 0)
//            .WithMessage("شناسه مسیر داخلی شرکت (CompanyDomesticPathId) باید بزرگ‌تر از صفر باشد.")
//            .Must(item => item.Weight > 0)
//            .WithMessage("وزن (Weight) باید بزرگ‌تر از صفر باشد.")
//            .Must(item => (int)item.PathStructType > 0)
//            .WithMessage("نوع ساختار مسیر (PathStructType) باید بزرگ‌تر از صفر باشد.")
//            .Must(item => (int)item.WeightType > 0)
//            .WithMessage("نوع وزن (WeightType) باید بزرگ‌تر از صفر باشد.");
//    }
//}