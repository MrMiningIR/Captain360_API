namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Create;

public record CreateCompanyDomesticPathListStructPriceCommand(List<CreateCompanyDomesticPathStructPrice> CreateCompanyDomesticPathStructPrices);

//public class CreateCompanyDomesticPathListStructPriceCommandValidator : AbstractValidator<CreateCompanyDomesticPathListStructPriceCommand>
//{
//    public CreateCompanyDomesticPathListStructPriceCommandValidator()
//    {
//        // شرط 1: لیست نباید خالی باشد
//        RuleFor(command => command.CreateCompanyDomesticPathStructPrices)
//            .NotEmpty()
//            .WithMessage("لیست قیمت‌ها نمی‌تواند خالی باشد.");

//        // شرط 2: حداقل باید 2 آیتم وجود داشته باشد
//        RuleFor(command => command.CreateCompanyDomesticPathStructPrices)
//            .Must(prices => prices.Count >= 2)
//            .WithMessage("حداقل باید 2 آیتم در لیست وجود داشته باشد.");

//        // شرط 3: اولین آیتم باید WeightTypeOne باشد
//        RuleFor(command => command.CreateCompanyDomesticPathStructPrices)
//            .Must(prices => prices.Any() && prices.First().WeightType == WeightType.TypeMin)
//            .WithMessage("اولین آیتم باید از نوع WeightTypeOne باشد.");

//        // شرط 4: دومین آیتم باید WeightTypeTwo باشد
//        RuleFor(command => command.CreateCompanyDomesticPathStructPrices)
//            .Must(prices => prices.Count >= 2 && prices.ElementAt(1).WeightType == WeightType.TypeTwo)
//            .WithMessage("دومین آیتم باید از نوع WeightTypeTwo باشد.");

//        // شرط 5: وزن‌ها باید به ترتیب افزایشی باشند
//        RuleFor(command => command.CreateCompanyDomesticPathStructPrices)
//            .Must(prices =>
//            {
//                for (int i = 0; i < prices.Count - 1; i++)
//                {
//                    if (prices[i].Weight >= prices[i + 1].Weight)
//                        return false;
//                }
//                return true;
//            })
//            .WithMessage(prices =>
//            {
//                var invalidItems = new List<string>();
//                for (int i = 0; i < prices.CreateCompanyDomesticPathStructPrices.Count - 1; i++)
//                {
//                    if (prices.CreateCompanyDomesticPathStructPrices[i].Weight >= prices.CreateCompanyDomesticPathStructPrices[i + 1].Weight)
//                    {
//                        invalidItems.Add($"آیتم در اندیس {i}: Weight={prices.CreateCompanyDomesticPathStructPrices[i].Weight}, WeightType={prices.CreateCompanyDomesticPathStructPrices[i].WeightType}");
//                        invalidItems.Add($"آیتم در اندیس {i + 1}: Weight={prices.CreateCompanyDomesticPathStructPrices[i + 1].Weight}, WeightType={prices.CreateCompanyDomesticPathStructPrices[i + 1].WeightType}");
//                    }
//                }
//                return $"وزن‌ها باید به ترتیب افزایشی باشند. آیتم‌های مشکل‌دار: {string.Join("; ", invalidItems)}";
//            });

//        // شرط 6: ترتیب WeightType باید افزایشی باشد و فاصله‌ای بین آن‌ها وجود نداشته باشد
//        RuleFor(command => command.CreateCompanyDomesticPathStructPrices)
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
//            .WithMessage(prices =>
//            {
//                var weightTypes = prices.CreateCompanyDomesticPathStructPrices.Select(p => (int)p.WeightType).ToList();
//                var invalidItems = new List<string>();
//                for (int i = 0; i < weightTypes.Count - 1; i++)
//                {
//                    if (weightTypes[i + 1] != weightTypes[i] + 1)
//                    {
//                        invalidItems.Add($"آیتم در اندیس {i}: WeightType={prices.CreateCompanyDomesticPathStructPrices[i].WeightType}");
//                        invalidItems.Add($"آیتم در اندیس {i + 1}: WeightType={prices.CreateCompanyDomesticPathStructPrices[i + 1].WeightType}");
//                    }
//                }
//                return $"ترتیب WeightType باید افزایشی باشد و هیچ فاصله‌ای بین آن‌ها وجود نداشته باشد. آیتم‌های مشکل‌دار: {string.Join("; ", invalidItems)}";
//            });

//        // شرط 7: اعتبارسنجی فیلدهای CreateCompanyDomesticPathStructPriceItem
//        RuleForEach(command => command.CreateCompanyDomesticPathStructPrices)
//            .Must(item => item.CompanyDomesticPathId > 0)
//            .WithMessage("شناسه مسیر داخلی شرکت (CompanyDomesticPathId) باید بزرگ‌تر از صفر باشد.")
//            .Must(item => item.Weight > 0)
//            .WithMessage("وزن (Weight) باید بزرگ‌تر از صفر باشد.")
//            .Must(item => (int)item.PathStructType > 0)
//            .WithMessage("نوع ساختار مسیر (PathStructType) باید بزرگ‌تر از صفر باشد.")
//            .Must(item => (int)item.WeightType > 0)
//            .WithMessage("نوع وزن (WeightType) باید بزرگ‌تر از صفر باشد.");

//        // شرط 8: StructPriceArea می‌تواند خالی باشد، اما اگر خالی نیست باید معتبر باشد
//        RuleForEach(command => command.CreateCompanyDomesticPathStructPrices)
//            .ChildRules(item =>
//            {
//                item.RuleFor(x => x.StructPriceArea)
//                    .SetValidator(new CreateCompanyDomesticPathStructPriceMunicipalAreasCommandValidator()!)
//                    .When(x => x.StructPriceArea != null && x.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Any());
//            });

//        // شرط 9: تطابق WeightType بین CreateCompanyDomesticPathStructPriceItem و StructPriceArea
//        RuleForEach(command => command.CreateCompanyDomesticPathStructPrices)
//            .Must(item =>
//            {
//                if (item.StructPriceArea == null || !item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Any() || !item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Any())
//                    return true; // StructPriceArea می‌تواند خالی باشد

//                // بررسی تطابق WeightType
//                var areaWeightTypes = item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Select(a => a.WeightType).ToList();
//                return areaWeightTypes.Contains(item.WeightType);
//            })
//            .WithMessage(item => $"آیتم با WeightType={item.CreateCompanyDomesticPathStructPrices.First().WeightType} باید حداقل یک آیتم مرتبط با همان WeightType در StructPriceArea داشته باشد.");
//    }
//}

//public class CreateCompanyDomesticPathStructPriceMunicipalAreasCommandValidator : AbstractValidator<CreateCompanyDomesticPathStructPriceMunicipalAreasCommand>
//{
//    public CreateCompanyDomesticPathStructPriceMunicipalAreasCommandValidator()
//    {
//        // شرط 1: اگر لیست خالی نیست، آیتم‌ها باید معتبر باشند
//        RuleFor(command => command.DomesticPathStructPriceMunicipalAreas)
//            .NotNull()
//            .WithMessage("لیست آیتم‌های StructPriceArea نمی‌تواند null باشد.");

//        // شرط 2: اعتبارسنجی فیلدهای CreateCompanyDomesticPathStructPriceMunicipalAreasItem
//        RuleForEach(command => command.DomesticPathStructPriceMunicipalAreas)
//            .ChildRules(item =>
//            {
//                item.RuleFor(x => x.CompanyDomesticPathId)
//                    .GreaterThan(0)
//                    .WithMessage("شناسه مسیر داخلی شرکت (CompanyDomesticPathId) باید بزرگ‌تر از صفر باشد.");

//                item.RuleFor(x => x.MunicipalAreaId)
//                    .GreaterThan(0)
//                    .WithMessage("شناسه منطقه شهری (MunicipalAreaId) باید بزرگ‌تر از صفر باشد.");

//                item.RuleFor(x => (int)x.PathStructType)
//                    .GreaterThan(0)
//                    .WithMessage("نوع ساختار مسیر (PathStructType) باید بزرگ‌تر از صفر باشد.");

//                item.RuleFor(x => (int)x.WeightType)
//                    .GreaterThan(0)
//                    .WithMessage("نوع وزن (WeightType) باید بزرگ‌تر از صفر باشد.");

//                item.RuleFor(x => x.Price)
//                    .GreaterThanOrEqualTo(0)
//                    .WithMessage(x => $"قیمت (Price) برای آیتم با WeightType={x.WeightType} نباید منفی باشد.");

//                item.RuleFor(x => x.CompanyDomesticPathStructPriceId)
//                    .GreaterThanOrEqualTo(0)
//                    .WithMessage("شناسه قیمت ساختار مسیر (CompanyDomesticPathStructPriceId) باید بزرگ‌تر یا برابر با صفر باشد.");
//            });
//    }
//}