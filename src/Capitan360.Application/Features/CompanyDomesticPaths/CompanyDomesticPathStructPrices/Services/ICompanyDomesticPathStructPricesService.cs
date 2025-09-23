using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Delete;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Dtos;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Queries.GetAll;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Queries.GetById;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Queries.GetTableDataQuery;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Services;

public interface ICompanyDomesticPathStructPricesService
{
    Task<ApiResponse<List<int>>> CreateStructPathByList(CreateCompanyDomesticPathListStructPriceCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<List<int>>> UpdateCompanyDomesticPathStructPriceAsync(UpdateCompanyDomesticPathListStructPriceCommand command, CancellationToken cancellationToken);
    //  Task<ApiResponse<List<int>>> UpdateCompanyDomesticPathStructPriceAsync(UpdateCompanyDomesticPathStructPriceCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<int>> CreateCompanyDomesticPathStructPriceAsync(CreateCompanyDomesticPathStructPriceCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<PagedResult<CompanyDomesticPathStructPriceDto>>> GetAllCompanyDomesticPathStructPrices(GetAllCompanyDomesticPathStructQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<CompanyDomesticPathStructPriceDto>> GetCompanyDomesticPathStructPriceByIdAsync(GetCompanyDomesticPathStructPriceByIdQuery query, CancellationToken cancellationToken);
    Task<ApiResponse<object>> DeleteCompanyDomesticPathStructPriceAsync(DeleteCompanyDomesticPathStructPriceCommand command, CancellationToken cancellationToken);
    Task<ApiResponse<List<TableDataDto>>> GetTableDataAsync(GetCompanyDomesticPathStructPriceTableDataQuery query,
        CancellationToken cancellationToken);


}

//public class UpdateCompanyDomesticPathListStructPriceCommandValidator : AbstractValidator<UpdateCompanyDomesticPathListStructPriceCommand>
//{
//    public UpdateCompanyDomesticPathListStructPriceCommandValidator()
//    {
//        #region MyRegion
//        // شرط 1: لیست نباید خالی باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .NotEmpty()
//            .WithMessage("لیست قیمت‌ها برای به‌روزرسانی نمی‌تواند خالی باشد.");

//        // شرط 2: حداقل باید 2 وزن معتبر (غیرصفر) وجود داشته باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices => prices.Count(p => p.Weight > 0) >= 2)
//            .WithMessage("حداقل باید 2 وزن معتبر (غیرصفر) در لیست وجود داشته باشد.");

//        // شرط 3: وزن‌های مربوط به WeightTypeOne و WeightTypeTwo باید وجود داشته باشند و وزن غیرصفر داشته باشند
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var hasWeightTypeOne = prices.Any(p => p.WeightType == WeightType.TypeMin && p.Weight > 0);
//                var hasWeightTypeTwo = prices.Any(p => p.WeightType == WeightType.TypeTwo && p.Weight > 0);
//                return hasWeightTypeOne && hasWeightTypeTwo;
//            })
//            .WithMessage("وزن‌های مربوط به WeightTypeOne و WeightTypeTwo باید وجود داشته باشند و وزن آنها نمی‌تواند صفر باشد.");

//        // شرط 4: وزن‌ها باید به ترتیب افزایشی باشند (فقط برای وزن‌های غیرصفر)
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var validPrices = prices.Where(p => p.Weight > 0)
//                    .OrderBy(p => p.WeightType)
//                    .ToList();
//                for (int i = 0; i < validPrices.Count - 1; i++)
//                {
//                    if (validPrices[i].Weight >= validPrices[i + 1].Weight)
//                        return false;
//                }
//                return true;
//            })
//            .WithMessage(prices =>
//            {
//                var validPrices = prices.UpdateCompanyDomesticPathStructPriceItems
//                    .Where(p => p.Weight > 0)
//                    .OrderBy(p => p.WeightType)
//                    .ToList();
//                var invalidItems = new List<string>();
//                for (int i = 0; i < validPrices.Count - 1; i++)
//                {
//                    if (validPrices[i].Weight >= validPrices[i + 1].Weight)
//                    {
//                        invalidItems.Add($"آیتم با Id={validPrices[i].Id ?? 0}, Weight={validPrices[i].Weight}, WeightType={validPrices[i].WeightType}");
//                        invalidItems.Add($"آیتم با Id={validPrices[i + 1].Id ?? 0}, Weight={validPrices[i + 1].Weight}, WeightType={validPrices[i + 1].WeightType}");
//                    }
//                }
//                return $"وزن‌های معتبر باید به ترتیب افزایشی باشند. آیتم‌های مشکل‌دار: {string.Join("; ", invalidItems)}";
//            });

//        // شرط 5: ترتیب WeightType باید افزایشی باشد و هیچ فاصله‌ای بین آنها وجود نداشته باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var validPrices = prices.Where(p => p.Weight > 0)
//                    .OrderBy(p => p.WeightType)
//                    .ToList();
//                var weightTypes = validPrices.Select(p => (int)p.WeightType).ToList();
//                for (int i = 0; i < weightTypes.Count - 1; i++)
//                {
//                    if (weightTypes[i + 1] != weightTypes[i] + 1)
//                        return false;
//                }
//                return true;
//            })
//            .WithMessage(prices =>
//            {
//                var validPrices = prices.UpdateCompanyDomesticPathStructPriceItems
//                    .Where(p => p.Weight > 0)
//                    .OrderBy(p => p.WeightType)
//                    .ToList();
//                var weightTypes = validPrices.Select(p => (int)p.WeightType).ToList();
//                var invalidItems = new List<string>();
//                for (int i = 0; i < weightTypes.Count - 1; i++)
//                {
//                    if (weightTypes[i + 1] != weightTypes[i] + 1)
//                    {
//                        invalidItems.Add($"آیتم با Id={validPrices[i].Id ?? 0}, Weight={validPrices[i].Weight}, WeightType={validPrices[i].WeightType}");
//                        invalidItems.Add($"آیتم با Id={validPrices[i + 1].Id ?? 0}, Weight={validPrices[i + 1].Weight}, WeightType={validPrices[i + 1].WeightType}");
//                    }
//                }
//                return $"ترتیب WeightType باید افزایشی باشد و هیچ فاصله‌ای بین آنها وجود نداشته باشد. آیتم‌های مشکل‌دار: {string.Join("; ", invalidItems)}";
//            });

//        // شرط 6: بررسی فیلدهای اجباری برای هر آیتم
//        RuleForEach(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(item => item.Id == null || item.Id >= 0)
//            .WithMessage("شناسه قیمت (Id) باید معتبر باشد (null یا بزرگ‌تر یا برابر با صفر).")
//            .Must(item => item.CompanyDomesticPathId > 0)
//            .WithMessage("شناسه مسیر داخلی شرکت (CompanyDomesticPathId) باید بزرگ‌تر از صفر باشد.")
//            .Must(item => (int)item.WeightType > 0)
//            .WithMessage("نوع وزن (WeightType) باید معتبر باشد.");
//        // شرط 8: StructPriceArea می‌تواند خالی باشد، اما اگر خالی نیست باید معتبر باشد
//        RuleForEach(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .ChildRules(item =>
//            {
//                item.RuleFor(x => x.StructPriceArea)
//                    .SetValidator(new UpdateCompanyDomesticPathStructPriceMunicipalAreasCommandValidator()!)
//                    .When(x => x.StructPriceArea?.DomesticPathStructPriceMunicipalAreas.Count > 0);
//            });

//        // شرط 9: تطابق WeightType بین CreateCompanyDomesticPathStructPriceItem و StructPriceArea
//        RuleForEach(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(item =>
//            {

//                if (item.StructPriceArea is null || !item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Any())
//                    return true; // StructPriceArea می‌تواند خالی باشد

//                // بررسی تطابق WeightType
//                var areaWeightTypes = item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Select(a => a.WeightType).ToList();
//                return areaWeightTypes.Contains(item.WeightType);
//            })
//            .WithMessage(item => $"آیتم با WeightType={item.UpdateCompanyDomesticPathStructPriceItems.First().WeightType} باید حداقل یک آیتم مرتبط با همان WeightType در StructPriceArea داشته باشد.");


//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must((command, items) =>
//            {
//                var validWeightTypes = items
//                    .Where(i => i.Weight > 0 && (i.Id > 0 || i.Id == null || i.Id == 0))
//                    .Select(i => i.WeightType)
//                    .Distinct()
//                    .ToList();

//                // اگه هیچ آیتمی در MunicipalAreas وجود نداشته باشه, معتبره
//                var hasMunicipalItems = items.Any(item =>
//                    item.StructPriceArea != null &&
//                    item.StructPriceArea.DomesticPathStructPriceMunicipalAreas != null &&
//                    item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Any());

//                if (!hasMunicipalItems)
//                    return true;

//                // اگه MunicipalAreas وجود داره, باید همه WeightTypeهای معتبر پوشش داده بشن
//                foreach (var weightType in validWeightTypes)
//                {
//                    var hasMatchingMunicipalItem = items.Any(item =>
//                        item.StructPriceArea != null &&
//                        item.StructPriceArea.DomesticPathStructPriceMunicipalAreas != null &&
//                        item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Any(a => a.WeightType == weightType && a.Price != 0)
//                    );

//                    if (!hasMatchingMunicipalItem)
//                        return false;
//                }

//                // بررسی تعداد WeightTypeهای غیرحذف در MunicipalAreas
//                var municipalWeightTypes = items
//                    .Where(item => item.StructPriceArea != null && item.StructPriceArea.DomesticPathStructPriceMunicipalAreas != null)
//                    .SelectMany(item => item.StructPriceArea.DomesticPathStructPriceMunicipalAreas)
//                    .Where(a => a.Price != 0)
//                    .Select(a => a.WeightType)
//                    .Distinct()
//                    .ToList();

//                return municipalWeightTypes.Count == validWeightTypes.Count &&
//                       validWeightTypes.All(wt => municipalWeightTypes.Contains(wt));
//            })
//            .WithMessage(command =>
//            {
//                var validWeightTypes = command.UpdateCompanyDomesticPathStructPriceItems
//                    .Where(i => i.Weight > 0 && (i.Id > 0 || i.Id == null || i.Id == 0))
//                    .Select(i => i.WeightType)
//                    .Distinct()
//                    .ToList();
//                var missingWeightTypes = new List<string>();
//                var municipalWeightTypes = command.UpdateCompanyDomesticPathStructPriceItems
//                    .Where(item => item.StructPriceArea != null && item.StructPriceArea.DomesticPathStructPriceMunicipalAreas != null)
//                    .SelectMany(item => item.StructPriceArea.DomesticPathStructPriceMunicipalAreas)
//                    .Where(a => a.Price != 0)
//                    .Select(a => a.WeightType)
//                    .Distinct()
//                    .ToList();

//                foreach (var weightType in validWeightTypes)
//                {
//                    if (!municipalWeightTypes.Contains(weightType))
//                        missingWeightTypes.Add(weightType.ToString());
//                }

//                if (missingWeightTypes.Any())
//                    return $"برای WeightTypeهای {string.Join(", ", missingWeightTypes)} هیچ آیتم غیرحذف مرتبطی در DomesticPathStructPriceMunicipalAreas یافت نشد.";
//                return $"تعداد WeightTypeهای غیرحذف در DomesticPathStructPriceMunicipalAreas ({municipalWeightTypes.Count}) با تعداد WeightTypeهای معتبر ({validWeightTypes.Count}) مطابقت ندارد.";
//            });

//        #endregion







//    }
//}


//public class UpdateCompanyDomesticPathListStructPriceCommandValidator : AbstractValidator<UpdateCompanyDomesticPathListStructPriceCommand>
//{
//    public UpdateCompanyDomesticPathListStructPriceCommandValidator()
//    {
//        // شرط 1: لیست نباید خالی باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .NotEmpty()
//            .WithMessage("لیست قیمت‌ها برای به‌روزرسانی نمی‌تواند خالی باشد.");

//        // شرط 2: حداقل باید 2 وزن معتبر (غیرصفر) وجود داشته باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices => prices.Count(p => p.Weight > 0) >= 2)
//            .WithMessage("حداقل باید 2 وزن معتبر (غیرصفر) در لیست وجود داشته باشد.");

//        // شرط 3: وزن‌های مربوط به WeightTypeOne و WeightTypeTwo باید وجود داشته باشند و وزن غیرصفر داشته باشند
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var hasWeightTypeOne = prices.Any(p => p.WeightType == WeightType.WeightTypeOne && p.Weight > 0);
//                var hasWeightTypeTwo = prices.Any(p => p.WeightType == WeightType.WeightTypeTwo && p.Weight > 0);
//                return hasWeightTypeOne && hasWeightTypeTwo;
//            })
//            .WithMessage("وزن‌های مربوط به WeightTypeOne و WeightTypeTwo باید وجود داشته باشند و وزن آنها نمی‌تواند صفر باشد.");

//        // شرط 4: وزن‌ها باید به ترتیب افزایشی باشند (فقط برای وزن‌های غیرصفر)
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var validPrices = prices.Where(p => p.Weight > 0)
//                    .OrderBy(p => p.WeightType)
//                    .ToList();
//                for (int i = 0; i < validPrices.Count - 1; i++)
//                {
//                    if (validPrices[i].Weight >= validPrices[i + 1].Weight)
//                        return false;
//                }
//                return true;
//            })
//            .WithMessage("وزن‌های معتبر باید به ترتیب افزایشی (از کم به زیاد) باشند.");

//        // شرط 5: ترتیب WeightType باید افزایشی باشد و فاصله‌ای بین آنها وجود نداشته باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var validPrices = prices.Where(p => p.Weight > 0)
//                    .OrderBy(p => p.WeightType)
//                    .ToList();
//                var weightTypes = validPrices.Select(p => (int)p.WeightType).ToList();
//                for (int i = 0; i < weightTypes.Count - 1; i++)
//                {
//                    if (weightTypes[i + 1] != weightTypes[i] + 1)
//                        return false;
//                }
//                return true;
//            })
//            .WithMessage("ترتیب WeightType باید افزایشی باشد و هیچ فاصله‌ای بین آنها وجود نداشته باشد.");

//        // شرط 6: بررسی فیلدهای اجباری برای هر آیتم
//        RuleForEach(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(item => item.Id == null || item.Id >= 0) // Id می‌تواند null یا 0 (برای درج) یا بزرگ‌تر از 0 (برای به‌روزرسانی) باشد
//            .WithMessage("شناسه قیمت (Id) باید معتبر باشد (null یا بزرگ‌تر یا برابر با صفر).")
//            .Must(item => item.CompanyDomesticPathId > 0)
//            .WithMessage("شناسه مسیر داخلی شرکت (CompanyDomesticPathId) باید بزرگ‌تر از صفر باشد.")
//            .Must(item => (int)item.WeightType > 0)
//            .WithMessage("نوع وزن (WeightType) باید معتبر باشد.");
//    }
//}
//public class UpdateCompanyDomesticPathListStructPriceCommandValidator : AbstractValidator<UpdateCompanyDomesticPathListStructPriceCommand>
//{
//    public UpdateCompanyDomesticPathListStructPriceCommandValidator()
//    {
//        // شرط 1: لیست نباید خالی باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .NotEmpty()
//            .WithMessage("لیست قیمت‌ها برای به‌روزرسانی نمی‌تواند خالی باشد.");

//        // شرط 2: حداقل باید 2 وزن معتبر (غیرصفر) وجود داشته باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices => prices.Count(p => p.Weight > 0) >= 2)
//            .WithMessage("حداقل باید 2 وزن معتبر (غیرصفر) در لیست وجود داشته باشد.");

//        // شرط 3: وزن‌های مربوط به WeightTypeOne و WeightTypeTwo باید وجود داشته باشند و وزن غیرصفر داشته باشند
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var hasWeightTypeOne = prices.Any(p => p.WeightType == WeightType.WeightTypeOne && p.Weight > 0);
//                var hasWeightTypeTwo = prices.Any(p => p.WeightType == WeightType.WeightTypeTwo && p.Weight > 0);
//                return hasWeightTypeOne && hasWeightTypeTwo;
//            })
//            .WithMessage("وزن‌های مربوط به WeightTypeOne و WeightTypeTwo باید وجود داشته باشند و وزن آنها نمی‌تواند صفر باشد.");

//        // شرط 4: وزن‌ها باید به ترتیب افزایشی باشند (فقط برای وزن‌های غیرصفر)
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var validPrices = prices.Where(p => p.Weight > 0)
//                    .OrderBy(p => p.WeightType) // مرتب‌سازی بر اساس WeightType برای اطمینان از ترتیب
//                    .ToList();
//                for (int i = 0; i < validPrices.Count - 1; i++)
//                {
//                    if (validPrices[i].Weight >= validPrices[i + 1].Weight)
//                        return false;
//                }
//                return true;
//            })
//            .WithMessage("وزن‌های معتبر باید به ترتیب افزایشی (از کم به زیاد) باشند.");

//        // شرط 5: ترتیب WeightType باید افزایشی باشد و فاصله‌ای بین آنها وجود نداشته باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var validPrices = prices.Where(p => p.Weight > 0)
//                    .OrderBy(p => p.WeightType)
//                    .ToList();
//                var weightTypes = validPrices.Select(p => (int)p.WeightType).ToList();
//                for (int i = 0; i < weightTypes.Count - 1; i++)
//                {
//                    if (weightTypes[i + 1] != weightTypes[i] + 1)
//                        return false;
//                }
//                return true;
//            })
//            .WithMessage("ترتیب WeightType باید افزایشی باشد و هیچ فاصله‌ای بین آنها وجود نداشته باشد.");

//        // شرط 6: بررسی فیلدهای اجباری
//        RuleForEach(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(item => item.Id > 0)
//            .WithMessage("شناسه قیمت (Id) باید بزرگ‌تر از صفر باشد.")
//            .Must(item => item.CompanyDomesticPathId > 0)
//            .WithMessage("شناسه مسیر داخلی شرکت (CompanyDomesticPathId) باید بزرگ‌تر از صفر باشد.")
//            .Must(item => (int)item.WeightType > 0)
//            .WithMessage("نوع وزن (WeightType) باید معتبر باشد.");
//    }
//}
//public class UpdateCompanyDomesticPathListStructPriceCommandValidator : AbstractValidator<UpdateCompanyDomesticPathListStructPriceCommand>
//{
//    //public UpdateCompanyDomesticPathListStructPriceCommandValidator()
//{



//    RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//        .NotEmpty()
//        .WithMessage("لیست قیمت‌ها برای به‌روزرسانی نمی‌تواند خالی باشد.");

//    // حداقل 2 وزن، حتی اگر برخی حذف شوند
//    RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//        .Must(prices => prices.Count(p => p.Weight > 0) >= 2)
//        .WithMessage("حداقل باید 2 وزن معتبر (غیرصفر) در لیست وجود داشته باشد.");



//    // شرط 3: اولین وزن باید WeightTypeOne باشد
//    RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//        .Must(prices => prices.Any() && prices.First().WeightType == WeightType.WeightTypeOne)
//        .WithMessage("اولین وزن باید از نوع WeightTypeOne باشد.");

//    // شرط 4: دومین وزن باید WeightTypeTwo باشد (اگر حداقل 2 وزن وجود دارد)
//    RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//        .Must(prices => prices.Count >= 2 && prices.ElementAt(1).WeightType == WeightType.WeightTypeTwo)
//        .WithMessage("دومین وزن باید از نوع WeightTypeTwo باشد.");

//    // وزن‌ها باید به ترتیب افزایشی باشند (فقط برای وزن‌های غیرصفر)
//    RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//        .Must(prices =>
//        {
//            var validWeights = prices.Where(p => p.Weight > 0).Select(p => p.Weight).ToList();
//            for (int i = 0; i < validWeights.Count - 1; i++)
//            {
//                if (validWeights[i] >= validWeights[i + 1])
//                    return false;
//            }
//            return true;
//        })
//        .WithMessage("وزن‌های معتبر باید به ترتیب افزایشی (از کم به زیاد) باشند.");

//    RuleForEach(command => command.UpdateCompanyDomesticPathStructPriceItems)
//        .Must(item => item.Id > 0)
//        .WithMessage("شناسه قیمت (Id) باید بزرگ‌تر از صفر باشد.");
//}


//    public UpdateCompanyDomesticPathListStructPriceCommandValidator()
//    {
//        // شرط 1: لیست نباید خالی باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .NotEmpty()
//            .WithMessage("لیست قیمت‌ها برای به‌روزرسانی نمی‌تواند خالی باشد.");

//        // شرط 2: حداقل باید 2 وزن معتبر (غیرصفر) وجود داشته باشد
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices => prices.Count(p => p.Weight > 0) >= 2)
//            .WithMessage("حداقل باید 2 وزن معتبر (غیرصفر) در لیست وجود داشته باشد.");

//        // شرط 3: وزن‌های اول و دوم (بر اساس WeightType) نمی‌توانند حذف شوند
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                // فرض: لیست بر اساس WeightType مرتب شده یا باید شامل WeightTypeOne و WeightTypeTwo باشد
//                var priceDict = prices.ToDictionary(p => p.Id, p => p.Weight);
//                var existingPrices = prices.Select(p => new { p.Id, p.Weight }).ToList(); // اینجا فرض می‌کنیم Idها به موجودیت‌های دیتابیس اشاره دارند
//                                                                                          // باید بررسی کنیم که Idهای مربوط به WeightTypeOne و WeightTypeTwo وزن معتبر داشته باشند
//                                                                                          // این نیاز به دسترسی به WeightType دارد، که در UpdateCompanyDomesticPathStructPriceItem نیست
//                                                                                          // بنابراین، فرض می‌کنیم لیست ورودی شامل تمام Idهای لازم است و دو آیتم اول معتبرند
//                var validPrices = prices.Where(p => p.Weight > 0).ToList();
//                return validPrices.Count >= 2 && validPrices.Take(2).All(p => p.Weight > 0);
//            })
//            .WithMessage("وزن‌های اول و دوم نمی‌توانند حذف شوند (وزن آنها باید غیرصفر باشد).");

//        // شرط 4: وزن‌ها باید به ترتیب افزایشی باشند (فقط برای وزن‌های غیرصفر)
//        RuleFor(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(prices =>
//            {
//                var validWeights = prices.Where(p => p.Weight > 0).Select(p => p.Weight).ToList();
//                for (int i = 0; i < validWeights.Count - 1; i++)
//                {
//                    if (validWeights[i] >= validWeights[i + 1])
//                        return false;
//                }
//                return true;
//            })
//            .WithMessage("وزن‌های معتبر باید به ترتیب افزایشی (از کم به زیاد) باشند.");

//        // شرط 5: بررسی Idهای معتبر
//        RuleForEach(command => command.UpdateCompanyDomesticPathStructPriceItems)
//            .Must(item => item.Id > 0)
//            .WithMessage("شناسه قیمت (Id) باید بزرگ‌تر از صفر باشد.");
//    }
//}



