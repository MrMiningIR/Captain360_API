using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.CreateCompanyDomesticPathCharge;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Commands.UpdateCompanyDomesticPathCharge;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Queries.GetAllCompanyDomesticPathCharge;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Create;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Commands.Delete;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Dtos;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Queries;
using Capitan360.Application.Services.CompanyServices.CompanyDomesticPathStructPrice.Services;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Repositories.CompanyRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyDomesticPathCharge.Services;

public class CompanyDomesticPathChargeService(ILogger<CompanyDomesticPathStructPricesService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyDomesticPathChargeRepository pathChargeRepository
    , ICompanyDomesticPathChargeContentTypeRepository contentTypeRepository,
    ICompanyDomesticPathsRepository domesticPathsRepository,
    ICompanyContentTypeRepository companyContentTypeRepository,
    IIdentityService identityService

    ) : ICompanyDomesticPathChargeService
{
    public async Task<ApiResponse<List<int>>> CreateCompanyDomesticPathCharge(CreateCompanyDomesticPathChargeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyDomesticPathChargeCommand is Called with {@CreateCompanyDomesticPathChargeCommand}", command);

        if (command == null || !command.ChargeItems.Any())
            return ApiResponse<List<int>>.Error(400, "لیست قیمت‌های ساختار مسیر نمی‌تواند null یا خالی باشد");

        if (command.ChargeItems.Any(a => a.CompanyDomesticPathId <= 0))
        {
            return ApiResponse<List<int>>.Error(400, "لیست شناسه مسیر  نمی‌تواند null یا خالی باشد");
        }

        if (command.ChargeItems.Any(a => a.Weight <= 0))
        {
            foreach (var item in command.ChargeItems.ToList())
            {
                if (item.Weight <= 0)
                {
                    command.ChargeItems.Remove(item);
                }
            }
        }
        if (command == null || !command.ChargeItems.Any())
            return ApiResponse<List<int>>.Error(400, "لیست قیمت‌های ساختار مسیر نمی‌تواند null یا خالی باشد");

        foreach (var item in command.ChargeItems)
        {
            //if (item.StructPriceArea == null)
            //    return ApiResponse<List<int>>.Error(400, $"StructPriceArea برای آیتم با CompanyDomesticPathId={item.CompanyDomesticPathId} نمی‌تواند null باشد");

            //if (item.StructPriceArea.DomesticPathStructPriceMunicipalAreas == null)
            //    return ApiResponse<List<int>>.Error(400, $"لیست DomesticPathStructPriceMunicipalAreas برای آیتم با CompanyDomesticPathId={item.CompanyDomesticPathId} نمی‌تواند null باشد");

            if (item.ContentItems?.ContentItemsList.Count > 0)
            {
                if (item.ContentItems.ContentItemsList.Any(a => a.CompanyDomesticPathId <= 0))
                    return ApiResponse<List<int>>.Error(400,
                        $"شناسه مسیر داخلی شرکت (CompanyDomesticPathId) در DomesticPathStructPriceMunicipalAreas برای آیتم با CompanyDomesticPathId={item.CompanyDomesticPathId} نمی‌تواند صفر یا منفی باشد");

                if (item.ContentItems.ContentItemsList.Any(a => a.ContentTypeId <= 0))
                    return ApiResponse<List<int>>.Error(400,
                        $"شناسه منطقه شهری (MunicipalAreaId) در DomesticPathStructPriceMunicipalAreas برای آیتم با CompanyDomesticPathId={item.CompanyDomesticPathId} نمی‌تواند صفر یا منفی باشد");

                if (item.ContentItems.ContentItemsList.Any(a => a.Price < 0))
                    return ApiResponse<List<int>>.Error(400,
                        $"قیمت (Price) در DomesticPathStructPriceMunicipalAreas برای آیتم با CompanyDomesticPathId={item.CompanyDomesticPathId} نمی‌تواند منفی باشد");
            }
            else
            {
                item.ContentItems = new CreateCompanyDomesticPathContentItemsCommand();
            }
        }

        //TODO
        var items = mapper.Map<List<Domain.Entities.CompanyEntity.CompanyDomesticPathCharge>>(command.ChargeItems);
        if (items == null || !items.Any())
            return ApiResponse<List<int>>.Error(500, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var pathChargeIds = await pathChargeRepository.CreateByListAsync(items, cancellationToken);
        if (pathChargeIds.Count <= 0 || !pathChargeIds.Any())
            return ApiResponse<List<int>>.Error(500, "خطا در ذخیره‌سازی CompanyDomesticPathStructPrices");
        logger.LogInformation("CompanyDomesticPathStructitems created successfully with IDs: {PriceIds}", string.Join(",", pathChargeIds));

        // مپینگ و ذخیره DomesticPathChargeContentType

        var contentTypeLists = new List<Domain.Entities.CompanyEntity.CompanyDomesticPathChargeContentType>();

        for (int i = 0; i < command.ChargeItems.Count; i++)
        {
            var commandItem = command.ChargeItems[i];
            var priceId = pathChargeIds[i];

            if (commandItem.ContentItems != null && commandItem.ContentItems.ContentItemsList.Any())
            {
                //TODO
                var mappedItems = mapper.Map<List<Domain.Entities.CompanyEntity.CompanyDomesticPathChargeContentType>>
                    (commandItem.ContentItems.ContentItemsList);

                foreach (var mappedItem in mappedItems)
                {
                    mappedItem.CompanyDomesticPathChargeId = priceId; // تنظیم کلید خارجی
                    contentTypeLists.Add(mappedItem);
                }
            }
        }

        if (contentTypeLists.Any())
        {
            var municipalAreaIds = await contentTypeRepository.Create(contentTypeLists, cancellationToken);
            logger.LogInformation("CompanyDomesticPathStructPriceMunicipalAreas created successfully with IDs: {MunicipalAreaIds}", string.Join(",", municipalAreaIds));
        }

        await unitOfWork.CommitTransactionAsync(cancellationToken);
        return ApiResponse<List<int>>.Created(pathChargeIds, "CompanyDomesticPathStructPrices و MunicipalAreas با موفقیت ایجاد شدند");
    }

    public async Task<ApiResponse<List<int>>> UpdateCompanyDomesticPathCharge(UpdateCompanyDomesticPathChargeCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyDomesticPathStructPrice is Called with {@UpdateCompanyDomesticPathListStructPriceCommand}", command);

        if (command == null || !command.ChargeItems.Any())
            return ApiResponse<List<int>>.Error(400, "لیست قیمت‌های ساختار مسیر برای به‌روزرسانی نمی‌تواند null یا خالی باشد");

        var (companyDomesticPathChargeUpdateList, companyDomesticPathChargeInsertList, companyDomesticPathChargeDeleteList) =
            ProcessItem(command.ChargeItems);

        // بررسی WeightTypeOne و WeightTypeTwo

        //TODO

        //if (command.UpdateCompanyDomesticPathStructPriceItems.Any(p => p.WeightType == WeightType.TypeMin && p.Weight <= 0) ||
        //    command.UpdateCompanyDomesticPathStructPriceItems.Any(p => p.WeightType == WeightType.TypeTwo && p.Weight <= 0))
        //    return ApiResponse<List<int>>.Error(400, "وزن‌های مربوط به WeightTypeOne و WeightTypeTwo نمی‌توانند صفر یا حذف شوند.");

        // جداسازی آیتم‌های به‌روزرسانی و درج

        var submittedPriceIds = companyDomesticPathChargeUpdateList.Select(i => i.Id!.Value).ToList();

        // شروع تراکنش
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var updatedPriceIds = new List<int>();
        var priceIdMap = new Dictionary<int, int>(); // برای نگاشت اندیس آیتم‌های جدید به Idهای واقعی

        try
        {
            // Handle Insert
            if (companyDomesticPathChargeInsertList.Any())
            {
                var newPrices = mapper.Map<List<Domain.Entities.CompanyEntity.CompanyDomesticPathCharge>>(companyDomesticPathChargeInsertList);
                for (int i = 0; i < newPrices.Count; i++)
                {
                    priceIdMap.Add(i, -1); // اندیس موقت
                }

                var insertedPriceIds = await pathChargeRepository.CreateByListAsync(newPrices, cancellationToken);
                updatedPriceIds.AddRange(insertedPriceIds);

                // نگاشت Idهای جدید
                for (int i = 0; i < insertedPriceIds.Count; i++)
                {
                    priceIdMap[i] = insertedPriceIds[i];
                }
            }

            // Handle Delete
            if (companyDomesticPathChargeDeleteList.Any())
            {
                var priceIdsToDelete = companyDomesticPathChargeDeleteList.Select(a => a.Id!.Value).ToList();

                await pathChargeRepository.DeleteByListAsync(priceIdsToDelete, cancellationToken);
                var deleteReferenceContentTypeIds = await contentTypeRepository.ShouldDeleteAsReference(priceIdsToDelete, cancellationToken);
                await contentTypeRepository.Delete(deleteReferenceContentTypeIds, cancellationToken);
                updatedPriceIds.AddRange(priceIdsToDelete);
            }
            // به‌روزرسانی رکوردهای موجود
            if (companyDomesticPathChargeUpdateList.Any())
            {
                var existingPrices = await pathChargeRepository.GetExistingStructPaths(submittedPriceIds, cancellationToken);

                //if (existingPrices.Count != submittedPriceIds.Count)
                //    return ApiResponse<List<int>>.Error(400, "یک یا چند قیمت با شناسه‌های ارائه‌شده یافت نشد");

                var pricesToUpdate = new List<Domain.Entities.CompanyEntity.CompanyDomesticPathCharge>();

                foreach (var item in companyDomesticPathChargeUpdateList)
                {
                    var existingPrice = existingPrices.First(p => p.Id == item.Id!.Value);

                    existingPrice.Weight = item.Weight;
                    existingPrice.PriceDirect = item.Price;
                    existingPrice.ContentTypeChargeBaseNormal = item.ContentTypeChargeBaseNormal;
                    pricesToUpdate.Add(existingPrice);
                }

                if (pricesToUpdate.Any())
                {
                    updatedPriceIds.AddRange(await pathChargeRepository.UpdateByListAsync(pricesToUpdate, cancellationToken));
                }
            }

            //  DomesticPathChargeContentType
            foreach (var item in command.ChargeItems)
            {
                if (item.ContentItems == null || item.ContentItems?.ContentItemsList == null)
                {
                    logger.LogWarning("StructPriceArea or DomesticPathStructPriceMunicipalAreas is null for item with Id={ItemId}", item.Id ?? 0);
                    return ApiResponse<List<int>>.Error(400, $"StructPriceArea یا DomesticPathStructPriceMunicipalAreas برای آیتم با Id={item.Id ?? 0} نمی‌تواند null باشد");
                }

                var contentItems = item.ContentItems.ContentItemsList;
                if (!contentItems.Any())
                {
                    logger.LogInformation("DomesticPathStructPriceMunicipalAreas is empty for item with Id={ItemId}", item.Id ?? 0);
                    continue;
                }

                // تعیین Id واقعی برای آیتم
                int actualPriceId;
                if (item.Id == 0 || item.Id == null)
                {
                    var index = command.ChargeItems
                        .Where(i => i.Id == 0 || i.Id == null)
                        .ToList()
                        .IndexOf(item);
                    if (!priceIdMap.ContainsKey(index))
                    {
                        logger.LogError("No PriceId found for new item at index {Index}", index);
                        throw new InvalidOperationException($"No PriceId found for new item at index {index}");
                    }
                    actualPriceId = priceIdMap[index];
                }
                else
                {
                    actualPriceId = item.Id.Value;
                }

                var (companyDomesticPathChargeContentUpdateList,
                    companyDomesticPathChargeContentInsertList,
                    companyDomesticPathChargeContentDeleteList)
                    = ProcessContentTypeItem(contentItems);

                // حذف آیتم‌ها
                if (companyDomesticPathChargeContentDeleteList.Any())
                {
                    var deleteIds = companyDomesticPathChargeContentDeleteList.Select(a => a.Id!.Value).ToList();
                    logger.LogInformation("Deleting MunicipalAreas items with IDs: {Ids}", string.Join(",", deleteIds));
                    await contentTypeRepository.Delete(deleteIds, cancellationToken);
                }

                // درج آیتم‌های جدید
                if (companyDomesticPathChargeContentInsertList.Any())
                {
                    var newContentTypeItems = mapper.Map<List<Domain.Entities.CompanyEntity.CompanyDomesticPathChargeContentType>>(companyDomesticPathChargeContentInsertList);
                    foreach (var newItem in newContentTypeItems)
                    {
                        newItem.CompanyDomesticPathChargeId = actualPriceId;
                    }
                    logger.LogInformation("Inserting {Count} MunicipalAreas items for item with Id={ItemId}", newContentTypeItems.Count, actualPriceId);
                    var insertedMunicipalIds = await contentTypeRepository
                        .Create(newContentTypeItems, cancellationToken);
                }

                // به‌روزرسانی آیتم‌های موجود
                if (companyDomesticPathChargeContentUpdateList.Any())
                {
                    var updateContentItems = mapper.Map<List<Domain.Entities.CompanyEntity.CompanyDomesticPathChargeContentType>>(companyDomesticPathChargeContentUpdateList);
                    foreach (var updateItem in updateContentItems)
                    {
                        updateItem.CompanyDomesticPathChargeId = actualPriceId;
                    }
                    logger.LogInformation("Updating {Count} MunicipalAreas items for item with Id={ItemId}", updateContentItems.Count, actualPriceId);
                    var updatedMunicipalIds = await contentTypeRepository.Update(updateContentItems, cancellationToken);
                }
            }

            // کامیت تراکنش
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing UpdateCompanyDomesticPathStructPriceAsync");
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            return ApiResponse<List<int>>.Error(500, "خطا در به‌روزرسانی یا درج داده‌ها");
        }

        logger.LogInformation("CompanyDomesticPathStructItems and MunicipalAreas updated/inserted/deleted successfully with IDs: {PriceIds}", string.Join(",", updatedPriceIds));
        return ApiResponse<List<int>>.Updated(updatedPriceIds, "CompanyDomesticPathStructItems و MunicipalAreas با موفقیت به‌روزرسانی/درج/حذف شدند");
    }

    public Task<ApiResponse<int>> CreateCompanyDomesticPathStructPriceAsync(CreateCompanyDomesticPathStructPriceCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<PagedResult<CompanyDomesticPathChargeDto>>> GetAllCompanyDomesticPathCharge(GetAllCompanyDomesticPathChargeQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyDomesticPathCharge is Called");
        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<CompanyDomesticPathChargeDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (items, totalCount) = await pathChargeRepository.GetMatchingAllCompanyDomesticPathCharge(
            query.SearchPhrase, query.CompanyDomesticPathId, query.PageSize, query.PageNumber,
            query.SortBy, query.SortDirection, cancellationToken);

        var priceDtos = mapper.Map<IReadOnlyList<CompanyDomesticPathChargeDto>>(items) ?? Array.Empty<CompanyDomesticPathChargeDto>();

        logger.LogInformation("Retrieved {Count} company domestic path struct items", priceDtos.Count);

        var data = new PagedResult<CompanyDomesticPathChargeDto>(priceDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyDomesticPathChargeDto>>.Ok(data, "Company domestic path struct items retrieved successfully");
    }

    public Task<ApiResponse<CompanyDomesticPathStructPriceDto>> GetCompanyDomesticPathStructPriceByIdAsync(GetCompanyDomesticPathStructPriceByIdQuery query,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<object>> DeleteCompanyDomesticPathStructPriceAsync(DeleteCompanyDomesticPathStructPriceCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<List<PathChargeTableDataDto>>> GetTableDataAAsync(GetCompanyDomesticPathChargeTableDataQuery query, CancellationToken cancellationToken)
    {
        if (query.CompanyDomesticPathId <= 0)
            return ApiResponse<List<PathChargeTableDataDto>>.Error(400, "مسیر وجود ندارد یا شناسه, و اطلاعات ارسالی ان اشتباه است");

        var domesticPth = await domesticPathsRepository.GetCompanyDomesticPathByIdAsync(query.CompanyDomesticPathId, false, false, cancellationToken);
        if (domesticPth is null)
            return ApiResponse<List<PathChargeTableDataDto>>.Error(400, "مسیر وجود ندارد یا شناسه ان اشتباه است");

        var (items, totalCount) = await pathChargeRepository.GetMatchingAllCompanyDomesticPathCharge(
    "", query.CompanyDomesticPathId, 100, 1,
     null, SortDirection.Ascending, cancellationToken);

        var (contentTypesData, total) = await companyContentTypeRepository
            .GetCompanyContentTypesAsync("", domesticPth.CompanyId, 1, 100, 1, null, SortDirection.Ascending, cancellationToken);

        var weightTypes = identityService.GetWeightTypeList();

        var tableData = new List<PathChargeTableDataDto>();

        // اضافه کردن ردیف بدون نام
        var unnamedRowWeight = new PathChargeTableDataDto
        {
            ContentTypeId = -1,
            ContentTypeName = "وزن",
            CompanyDomesticPathId = query.CompanyDomesticPathId, // پیش‌فرض
            Id = 0
        };
        tableData.Add(unnamedRowWeight);
        var unnamedRowPrice = new PathChargeTableDataDto
        {
            ContentTypeId = 0,
            ContentTypeName = "قیمت",
            CompanyDomesticPathId = query.CompanyDomesticPathId, // پیش‌فرض
            Id = 0
        };
        tableData.Add(unnamedRowPrice);

        // اضافه کردن ردیف‌های مناطق
        foreach (var content in contentTypesData)
        {
            tableData.Add(new PathChargeTableDataDto
            {
                ContentTypeId = content.ContentTypeId,
                ContentTypeName = content.CompanyContentTypeName ?? "",
                CompanyDomesticPathId = query.CompanyDomesticPathId,
                Id = 0
            });
        }

        // پر کردن داده‌های موجود
        foreach (var sub in items)
        {
            // ردیف بدون نام
            if (sub.ContentTypeId == 0)
            {
                var rowWeight = tableData.First(r => r.ContentTypeId == -1);
                rowWeight.Fields[(int)sub.WeightType] = new PathChargeFieldDto
                {
                    //  Weight = ((int)sub.WeightType == -1 || (int)sub.WeightType == 0) ? null : sub.Weight,
                    Weight = sub.Weight,
                    Price = null,
                    Static = false,
                    Id = sub.Id
                };
                var rowPrice = tableData.First(r => r.ContentTypeId == 0);
                rowPrice.Fields[(int)sub.WeightType] = new PathChargeFieldDto
                {
                    Weight = null,
                    Price = sub.PriceDirect,
                    Static = sub.ContentTypeChargeBaseNormal,
                    Id = sub.Id
                };
            }

            // ردیف‌های مناطق
            foreach (var item in sub.CompanyDomesticPathChargeContentTypes ?? [])
            {
                var row = tableData.FirstOrDefault(r => r.ContentTypeId == item.ContentTypeId);
                if (row != null)
                {
                    row.Fields[(int)item.WeightType] = new PathChargeFieldDto
                    {
                        Price = item.Price,
                        Id = item.Id,
                        CompanyDomesticPathChargeId = item.CompanyDomesticPathChargeId,
                        ContentTypeId = item.ContentTypeId,
                        CompanyDomesticPathId = row.CompanyDomesticPathId
                    };
                }
            }
        }

        //پر کردن فیلدهای خالی برای همه ردیف‌ها
        foreach (var row in tableData)
        {
            foreach (var weightType in weightTypes.Data!.Items.ToList())
            {
                if (!row.Fields.ContainsKey(weightType.Value))
                {
                    row.Fields[weightType.Value] = new PathChargeFieldDto
                    {
                        Weight = null,
                        Price = null,
                        Static = false,
                        Id = 0,
                        CompanyDomesticPathId = row.CompanyDomesticPathId,
                        ContentTypeId = row.ContentTypeId,
                        CompanyDomesticPathChargeId = 0
                    };
                }
            }
        }

        var data = tableData.OrderBy(r => r.ContentTypeId).ToList();

        return ApiResponse<List<PathChargeTableDataDto>>.Ok(data, "DomesticPathStructType Data Generated Successfully ");
    }

    public class PathChargeTableDataDto
    {
        public int Id { get; set; }
        public int ContentTypeId { get; set; }
        public string ContentTypeName { get; set; }
        public int CompanyDomesticPathId { get; set; }

        public Dictionary<int, PathChargeFieldDto> Fields { get; set; } = new();
    }

    public class PathChargeFieldDto
    {
        public int? Weight { get; set; } // برای بدون نام
        public long? Price { get; set; } // برای مناطق
        public bool Static { get; set; }
        public int Id { get; set; }
        public int CompanyDomesticPathChargeId { get; set; }
        public int ContentTypeId { get; set; }
        public int CompanyDomesticPathId { get; set; }
    }

    // Helper Methods

    private (
        List<UpdateCompanyDomesticPathChargeItemCommand> CompanyDomesticPathChargeUpdateList,
        List<UpdateCompanyDomesticPathChargeItemCommand> CompanyDomesticPathChargeInsertList,
        List<UpdateCompanyDomesticPathChargeItemCommand> CompanyDomesticPathChargeDeleteList
        )
        ProcessItem(List<UpdateCompanyDomesticPathChargeItemCommand> items)
    {
        var companyDomesticPathChargeUpdateList = new List<UpdateCompanyDomesticPathChargeItemCommand>();
        var companyDomesticPathChargeInsertList = new List<UpdateCompanyDomesticPathChargeItemCommand>();
        var companyDomesticPathChargeDeleteList = new List<UpdateCompanyDomesticPathChargeItemCommand>();

        foreach (var item in items)
        {
            // بررسی شرط نادیده گرفتن وزن
            bool ignoreWeightCondition = item.WeightType == WeightType.TypeMin || item.WeightType == WeightType.TypeNormal;

            if (item.Id is 0 or null)
            {
                if (item.Price > 0 && (ignoreWeightCondition || item.Weight > 0))
                {
                    companyDomesticPathChargeInsertList.Add(item);
                }
            }

            if (item.Id > 0)
            {
                if (item is { Price: <= 0, Weight: <= 0 })
                {
                    companyDomesticPathChargeDeleteList.Add(item);
                }
                else if (item.Price > 0 && (ignoreWeightCondition || item.Weight > 0))
                {
                    companyDomesticPathChargeUpdateList.Add(item);
                }
            }
        }
        // بازگشت نهایی
        return (companyDomesticPathChargeUpdateList, companyDomesticPathChargeInsertList, companyDomesticPathChargeDeleteList);
    }

    private (
    List<UpdateCompanyDomesticPathContentItemCommand> CompanyDomesticPathChargeContentUpdateList,
    List<UpdateCompanyDomesticPathContentItemCommand> CompanyDomesticPathChargeContentInsertList,
    List<UpdateCompanyDomesticPathContentItemCommand> CompanyDomesticPathChargeContentDeleteList
    )
    ProcessContentTypeItem(List<UpdateCompanyDomesticPathContentItemCommand> items)
    {
        var companyDomesticPathChargeContentTypeUpdateList = new List<UpdateCompanyDomesticPathContentItemCommand>();
        var companyDomesticPathChargeContentTypeInsertList = new List<UpdateCompanyDomesticPathContentItemCommand>();
        var companyDomesticPathChargeContentTypeDeleteList = new List<UpdateCompanyDomesticPathContentItemCommand>();

        foreach (var item in items)
        {
            if (item.Id is 0 or null)
            {
                if (item.Price > 0)
                {
                    companyDomesticPathChargeContentTypeInsertList.Add(item);
                }
            }

            if (item.Id > 0)
            {
                if (item is { Price: <= 0 })
                {
                    companyDomesticPathChargeContentTypeDeleteList.Add(item);
                }
                else if (item.Price > 0)
                {
                    companyDomesticPathChargeContentTypeUpdateList.Add(item);
                }
            }
        }

        return (companyDomesticPathChargeContentTypeUpdateList, companyDomesticPathChargeContentTypeInsertList, companyDomesticPathChargeContentTypeDeleteList);
    }

    public record GetCompanyDomesticPathChargeTableDataQuery(
       int CompanyDomesticPathId = 0

      );
}