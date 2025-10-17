using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Create;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Delete;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Commands.Update;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Dtos;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Queries.GetAll;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Queries.GetById;
using Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Queries.GetTableDataQuery;
using Capitan360.Application.Features.Dtos;
using Capitan360.Application.Features.Identities.Identities.Services;
using Capitan360.Domain.Entities.Addresses;
using Capitan360.Domain.Entities.CompanyDomesticPaths;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Interfaces.Repositories.Addresses;
using Capitan360.Domain.Interfaces.Repositories.CompanyDomesticPaths;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Features.CompanyDomesticPaths.CompanyDomesticPathStructPrices.Services;

public class CompanyDomesticPathStructPricesService(
    ILogger<CompanyDomesticPathStructPricesService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ICompanyDomesticPathStructPricesRepository companyDomesticPathStructPricesRepository,
    ICompanyDomesticPathStructPriceMunicipalAreasRepository companyDomesticPathStructPriceMunicipalAreasRepository,
    IIdentityService identityService,
    IAreaRepository areaRepository,
    ICompanyDomesticPathRepository domesticPathsRepository

    )
    : ICompanyDomesticPathStructPricesService
{
    private readonly IUserContext _userContext = userContext;

    public async Task<ApiResponse<List<int>>> CreateStructPathByList(
        CreateCompanyDomesticPathListStructPriceCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "CreateCompanyDomesticPathStructPrice is Called with {@CreateCompanyDomesticPathStructPriceCommand}",
            command);

        if (command == null || !command.CreateCompanyDomesticPathStructPrices.Any())
            return ApiResponse<List<int>>.Error(400, "لیست قیمت‌های ساختار مسیر نمی‌تواند null یا خالی باشد");
        if (command.CreateCompanyDomesticPathStructPrices.Any(a => a.CompanyDomesticPathId <= 0))
        {
            return ApiResponse<List<int>>.Error(400, "لیست شناسه مسیر  نمی‌تواند null یا خالی باشد");
        }

        if (command.CreateCompanyDomesticPathStructPrices.Any(a => a.Weight <= 0))
        {
            foreach (var item in command.CreateCompanyDomesticPathStructPrices.ToList())
            {
                if (item.Weight <= 0)
                {
                    command.CreateCompanyDomesticPathStructPrices.Remove(item);
                }
            }
        }

        if (command == null || !command.CreateCompanyDomesticPathStructPrices.Any())
            return ApiResponse<List<int>>.Error(400, "لیست قیمت‌های ساختار مسیر نمی‌تواند null یا خالی باشد");




        // اعتبارسنجی StructPriceArea
        foreach (var item in command.CreateCompanyDomesticPathStructPrices)
        {
            //if (item.StructPriceArea == null)
            //    return ApiResponse<List<int>>.Error(400, $"StructPriceArea برای آیتم با CompanyDomesticPathId={item.CompanyDomesticPathId} نمی‌تواند null باشد");

            //if (item.StructPriceArea.DomesticPathStructPriceMunicipalAreas == null)
            //    return ApiResponse<List<int>>.Error(400, $"لیست DomesticPathStructPriceMunicipalAreas برای آیتم با CompanyDomesticPathId={item.CompanyDomesticPathId} نمی‌تواند null باشد");

            if (item.StructPriceArea?.DomesticPathStructPriceMunicipalAreas.Count > 0)
            {
                if (item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Any(a => a.CompanyDomesticPathId <= 0))
                    return ApiResponse<List<int>>.Error(400,
                        $"شناسه مسیر داخلی شرکت (CompanyDomesticPathId) در DomesticPathStructPriceMunicipalAreas برای آیتم با CompanyDomesticPathId={item.CompanyDomesticPathId} نمی‌تواند صفر یا منفی باشد");

                if (item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Any(a => a.MunicipalAreaId <= 0))
                    return ApiResponse<List<int>>.Error(400,
                        $"شناسه منطقه شهری (MunicipalAreaId) در DomesticPathStructPriceMunicipalAreas برای آیتم با CompanyDomesticPathId={item.CompanyDomesticPathId} نمی‌تواند صفر یا منفی باشد");

                if (item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Any(a => a.Price < 0))
                    return ApiResponse<List<int>>.Error(400,
                        $"قیمت (Price) در DomesticPathStructPriceMunicipalAreas برای آیتم با CompanyDomesticPathId={item.CompanyDomesticPathId} نمی‌تواند منفی باشد");
            }
            else
            {
                item.StructPriceArea = new CreateCompanyDomesticPathStructPriceMunicipalAreasCommand();
            }
        }


        var items = mapper.Map<List<CompanyDomesticPathStructPrice>>(command.CreateCompanyDomesticPathStructPrices);
        if (items == null || !items.Any())
            return ApiResponse<List<int>>.Error(500, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var structPriceIds =
            await companyDomesticPathStructPricesRepository.CreateCompanyDomesticPathStructPriceAsync(items,
                cancellationToken);
        if (structPriceIds == null || !structPriceIds.Any())
            return ApiResponse<List<int>>.Error(500, "خطا در ذخیره‌سازی CompanyDomesticPathStructPrices");
        logger.LogInformation("CompanyDomesticPathStructitems created successfully with IDs: {PriceIds}",
            string.Join(",", structPriceIds));



        //// مپینگ و ذخیره DomesticPathStructPriceMunicipalAreas
        var municipalAreasItems =
            new List<CompanyDomesticPathStructPriceMunicipalArea>();
        for (int i = 0; i < command.CreateCompanyDomesticPathStructPrices.Count; i++)
        {
            var commandItem = command.CreateCompanyDomesticPathStructPrices[i];
            var priceId = structPriceIds[i];

            if (commandItem.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Any())
            {
                var mappedItems =
                    mapper.Map<List<CompanyDomesticPathStructPriceMunicipalArea>>(
                        commandItem.StructPriceArea.DomesticPathStructPriceMunicipalAreas);

                foreach (var mappedItem in mappedItems)
                {
                    mappedItem.CompanyDomesticPathStructPriceId = priceId; // تنظیم کلید خارجی
                    municipalAreasItems.Add(mappedItem);
                }
            }
        }

        if (municipalAreasItems.Any())
        {
            var municipalAreaIds =
                await companyDomesticPathStructPriceMunicipalAreasRepository.Create(municipalAreasItems,
                    cancellationToken);
            logger.LogInformation(
                "CompanyDomesticPathStructPriceMunicipalAreas created successfully with IDs: {MunicipalAreaIds}",
                string.Join(",", municipalAreaIds));
        }

        await unitOfWork.CommitTransactionAsync(cancellationToken);
        return ApiResponse<List<int>>.Created(structPriceIds,
            "CompanyDomesticPathStructPrices و MunicipalAreas با موفقیت ایجاد شدند");
    }

    public async Task<ApiResponse<PagedResult<CompanyDomesticPathStructPriceDto>>> GetAllCompanyDomesticPathStructPrices(GetAllCompanyDomesticPathStructQuery query,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyDomesticPathStructQuery is Called");
        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<CompanyDomesticPathStructPriceDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (items, totalCount) = await companyDomesticPathStructPricesRepository.GetAllCompanyDomesticPathStructPrice(
            query.SearchPhrase, query.CompanyDomesticPathId, query.PathStruct, query.PageSize, query.PageNumber,
            query.SortBy, query.SortDirection, cancellationToken);

        var priceDtos = mapper.Map<IReadOnlyList<CompanyDomesticPathStructPriceDto>>(items) ?? Array.Empty<CompanyDomesticPathStructPriceDto>();

        logger.LogInformation("Retrieved {Count} company domestic path struct items", priceDtos.Count);

        var data = new PagedResult<CompanyDomesticPathStructPriceDto>(priceDtos, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyDomesticPathStructPriceDto>>.Ok(data, "Company domestic path struct items retrieved successfully");
    }




    public async Task<ApiResponse<CompanyDomesticPathStructPriceDto>> GetCompanyDomesticPathStructPriceByIdAsync(
        GetCompanyDomesticPathStructPriceByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyDomesticPathStructPriceById is Called with ID: {Id}", query.Id);
        if (query.Id <= 0)
            return ApiResponse<CompanyDomesticPathStructPriceDto>.Error(400, "شناسه قیمت ساختار مسیر باید بزرگ‌تر از صفر باشد");

        var price = await companyDomesticPathStructPricesRepository.GetCompanyDomesticPathStructPriceById(query.Id, cancellationToken);
        if (price is null)
            return ApiResponse<CompanyDomesticPathStructPriceDto>.Error(400, $"قیمت ساختار مسیر با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<CompanyDomesticPathStructPriceDto>(price);
        logger.LogInformation("CompanyDomesticPathStructPrice retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyDomesticPathStructPriceDto>.Ok(result, "CompanyDomesticPathStructPrice retrieved successfully");
    }

    public async Task<ApiResponse<object>> DeleteCompanyDomesticPathStructPriceAsync(
        DeleteCompanyDomesticPathStructPriceCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyDomesticPathStructPrice is Called with ID: {Id}", command.Id);
        if (command.Id <= 0)
            return ApiResponse<object>.Error(400, "شناسه قیمت ساختار مسیر باید بزرگ‌تر از صفر باشد");

        var price = await companyDomesticPathStructPricesRepository.GetCompanyDomesticPathStructPriceById(command.Id, cancellationToken);
        if (price is null)
            return ApiResponse<object>.Error(400, $"قیمت ساختار مسیر با شناسه {command.Id} یافت نشد");

        companyDomesticPathStructPricesRepository.Delete(price);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyDomesticPathStructPrice soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<object>.Deleted("CompanyDomesticPathStructPrice deleted successfully");
    }



    public async Task<ApiResponse<List<int>>> UpdateCompanyDomesticPathStructPriceAsync(UpdateCompanyDomesticPathListStructPriceCommand command, CancellationToken cancellationToken)
    {

        #region MyRegion
        //logger.LogInformation("UpdateCompanyDomesticPathStructPrice is Called with {@UpdateCompanyDomesticPathListStructPriceCommand}", command);

        //if (command == null || !command.UpdateCompanyDomesticPathStructPriceItems.Any())
        //    return ApiResponse<List<int>>.Error(400, "لیست قیمت‌های ساختار مسیر برای به‌روزرسانی نمی‌تواند null یا خالی باشد");

        //// تنظیم Weight به 0 برای آیتم‌هایی که وزن نامعتبر دارند
        //foreach (var item in command.UpdateCompanyDomesticPathStructPriceItems)
        //{
        //    if (item is { Id: > 0, Weight: <= 0 })
        //    {
        //        item.Weight = 0;
        //    }
        //    if (item.Id is null or 0 && item.Weight > 0)
        //    {
        //        item.Id = 0;
        //        //   item.PathStructType = command.UpdateCompanyDomesticPathStructPriceItems.First(a => a.Id > 0 && a.Weight > 0).PathStructType;
        //    }

        //}

        //// بررسی WeightTypeOne و WeightTypeTwo
        //if (command.UpdateCompanyDomesticPathStructPriceItems.Any(p => p.WeightType == WeightType.WeightTypeOne && p.Weight <= 0) ||
        //    command.UpdateCompanyDomesticPathStructPriceItems.Any(p => p.WeightType == WeightType.WeightTypeTwo && p.Weight <= 0))
        //    return ApiResponse<List<int>>.Error(400, "وزن‌های مربوط به WeightTypeOne و WeightTypeTwo نمی‌توانند صفر یا حذف شوند.");

        //// جداسازی آیتم‌های به‌روزرسانی و درج
        //var updateItems = command.UpdateCompanyDomesticPathStructPriceItems
        //    .Where(i => i.Id is > 0)
        //    .ToList();
        //var insertItems = command.UpdateCompanyDomesticPathStructPriceItems
        //    .Where(i => i.Id is null or 0 && i.Weight > 0)
        //    .ToList();

        //var submittedPriceIds = updateItems.Select(i => i.Id!.Value).ToList();
        //var validItems = command.UpdateCompanyDomesticPathStructPriceItems.Where(i => i.Weight > 0).ToList();

        //// شروع تراکنش

        //await unitOfWork.BeginTransactionAsync(cancellationToken);


        //var updatedPriceIds = new List<int>();
        // var priceIdMap = new Dictionary<int, int>(); // برای نگاشت Idهای موقت به Idهای واقعی

        //// به‌روزرسانی رکوردهای موجود
        //if (updateItems.Any())
        //{
        //    var existingPrices = await companyDomesticPathStructPricesRepository.GetExistingStructPaths(submittedPriceIds, cancellationToken);

        //    //if (existingPrices.Count != submittedPriceIds.Count)
        //    //    return ApiResponse<List<int>>.Error(400, "یک یا چند قیمت با شناسه‌های ارائه‌شده یافت نشد");

        //    var pricesToUpdate = new List<CompanyDomesticPathStructPrices>();
        //    var priceIdsToDelete = new List<int>();

        //    foreach (var item in updateItems)
        //    {
        //        var existingPrice = existingPrices.First(p => p.Id == item.Id!.Value);
        //        if (item.Weight <= 0)
        //        {
        //            priceIdsToDelete.Add(item.Id!.Value);
        //        }
        //        else
        //        {
        //            existingPrice.Weight = item.Weight;
        //            pricesToUpdate.Add(existingPrice);
        //        }
        //    }

        //    if (pricesToUpdate.Any())
        //    {
        //        updatedPriceIds.AddRange(await companyDomesticPathStructPricesRepository.UpdateCompanyDomesticPathStructPriceAsync(pricesToUpdate, cancellationToken));
        //    }

        //    if (priceIdsToDelete.Any())
        //    {
        //        await companyDomesticPathStructPricesRepository.DeleteCompanyDomesticPathStructPricesAsync(priceIdsToDelete, cancellationToken);
        //        updatedPriceIds.AddRange(priceIdsToDelete);
        //    }
        //}

        //// درج رکوردهای جدید
        //if (insertItems.Any())
        //{
        //    var newPrices = mapper.Map<List<CompanyDomesticPathStructPrices>>(insertItems);
        //    var insertedPriceIds = await companyDomesticPathStructPricesRepository.CreateCompanyDomesticPathStructPriceAsync(newPrices, cancellationToken);
        //    updatedPriceIds.AddRange(insertedPriceIds);


        //}

        //// مدیریت DomesticPathStructPriceMunicipalAreas
        //foreach (var item in command.UpdateCompanyDomesticPathStructPriceItems)
        //{
        //    if (item.StructPriceArea == null || item.StructPriceArea.DomesticPathStructPriceMunicipalAreas == null || item.StructPriceArea.DomesticPathStructPriceMunicipalAreas.Count <= 0)
        //        continue; // اگر StructPriceArea یا لیست آن null باشد, پردازش نمی‌کنیم

        //    var municipalItems = item.StructPriceArea.DomesticPathStructPriceMunicipalAreas;

        //    // جداسازی آیتم‌ها برای حذف، درج و به‌روزرسانی
        //    var itemsToDelete = municipalItems
        //        .Where(a => a.Id > 0 && a.Price == 0)
        //        .Select(a => a.Id)
        //        .ToList();

        //    var itemsToInsert = municipalItems
        //        .Where(a => (a.Id == 0  && a.Price != 0))
        //        .ToList();

        //    var itemsToUpdate = municipalItems
        //        .Where(a => a.Id > 0 && a.Price != 0)
        //        .ToList();

        //    // حذف آیتم‌ها
        //    if (itemsToDelete.Any())
        //    {
        //        await companyDomesticPathStructPriceMunicipalAreasRepository.Delete(itemsToDelete, cancellationToken);
        //    }

        //    // درج آیتم‌های جدید
        //    if (itemsToInsert.Any())
        //    {
        //        var newMunicipalItems = mapper.Map<List<Domain.Entities.Companies.CompanyDomesticPathStructPriceMunicipalAreas>>(itemsToInsert);
        //        foreach (var newItem in newMunicipalItems)
        //        {
        //            newItem.CompanyDomesticPathStructPriceId = item.Id ?? 0; // تنظیم کلید خارجی
        //        }
        //        var insertedMunicipalIds = await companyDomesticPathStructPriceMunicipalAreasRepository.Create(newMunicipalItems, cancellationToken);
        //    }

        //    // به‌روزرسانی آیتم‌های موجود
        //    if (itemsToUpdate.Any())
        //    {
        //        var updateMunicipalItems = mapper.Map<List<Domain.Entities.Companies.CompanyDomesticPathStructPriceMunicipalAreas>>(itemsToUpdate);
        //        var updatedMunicipalIds = await companyDomesticPathStructPriceMunicipalAreasRepository.Update(updateMunicipalItems, cancellationToken);
        //    }
        //}



        //await unitOfWork.CommitTransactionAsync(cancellationToken);

        //logger.LogInformation("CompanyDomesticPathStructItems updated/inserted successfully with IDs: {PriceIds}", string.Join(",", updatedPriceIds));
        //return ApiResponse<List<int>>.Updated(updatedPriceIds, "CompanyDomesticPathStructItems updated/inserted successfully"); 
        #endregion

        logger.LogInformation("UpdateCompanyDomesticPathStructPrice is Called with {@UpdateCompanyDomesticPathListStructPriceCommand}", command);

        if (!command.UpdateCompanyDomesticPathStructPriceItems.Any())
            return ApiResponse<List<int>>.Error(400, "لیست قیمت‌های ساختار مسیر برای به‌روزرسانی نمی‌تواند null یا خالی باشد");

        // تنظیم Weight به 0 برای آیتم‌هایی که وزن نامعتبر دارند
        foreach (var item in command.UpdateCompanyDomesticPathStructPriceItems)
        {
            if (item is { Id: > 0, Weight: <= 0 })
            {
                item.Weight = 0;
            }
            if (item.Id is null or 0 && item.Weight > 0)
            {
                item.Id = 0;
                //   item.PathStructType = command.UpdateCompanyDomesticPathStructPriceItems.First(a => a.Id > 0 && a.Weight > 0).PathStructType;
            }
        }

        // بررسی WeightTypeOne و WeightTypeTwo
        if (command.UpdateCompanyDomesticPathStructPriceItems.Any(p => p.WeightType == WeightType.TypeMin && p.Weight <= 0) ||
            command.UpdateCompanyDomesticPathStructPriceItems.Any(p => p.WeightType == WeightType.TypeTwo && p.Weight <= 0))
            return ApiResponse<List<int>>.Error(400, "وزن‌های مربوط به WeightTypeOne و WeightTypeTwo نمی‌توانند صفر یا حذف شوند.");

        // جداسازی آیتم‌های به‌روزرسانی و درج
        var updateItems = command.UpdateCompanyDomesticPathStructPriceItems
            .Where(i => i.Id is > 0)
            .ToList();
        var insertItems = command.UpdateCompanyDomesticPathStructPriceItems
            .Where(i => i.Id is null or 0 && i.Weight > 0)
            .ToList();

        var submittedPriceIds = updateItems.Select(i => i.Id!.Value).ToList();
        var validItems = command.UpdateCompanyDomesticPathStructPriceItems.Where(i => i.Weight > 0).ToList();

        // شروع تراکنش
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var updatedPriceIds = new List<int>();
        var priceIdMap = new Dictionary<int, int>(); // برای نگاشت اندیس آیتم‌های جدید به Idهای واقعی

        try
        {
            // درج رکوردهای جدید
            if (insertItems.Any())
            {
                var newPrices = mapper.Map<List<CompanyDomesticPathStructPrice>>(insertItems);
                for (int i = 0; i < newPrices.Count; i++)
                {
                    priceIdMap.Add(i, -1); // اندیس موقت
                }

                var insertedPriceIds = await companyDomesticPathStructPricesRepository.CreateCompanyDomesticPathStructPriceAsync(newPrices, cancellationToken);
                updatedPriceIds.AddRange(insertedPriceIds);

                // نگاشت Idهای جدید
                for (int i = 0; i < insertedPriceIds.Count; i++)
                {
                    priceIdMap[i] = insertedPriceIds[i];
                }
            }

            // به‌روزرسانی رکوردهای موجود
            if (updateItems.Any())
            {
                var existingPrices = await companyDomesticPathStructPricesRepository.GetExistingStructPaths(submittedPriceIds, cancellationToken);

                //if (existingPrices.Count != submittedPriceIds.Count)
                //    return ApiResponse<List<int>>.Error(400, "یک یا چند قیمت با شناسه‌های ارائه‌شده یافت نشد");

                var pricesToUpdate = new List<CompanyDomesticPathStructPrice>();
                var priceIdsToDelete = new List<int>();

                foreach (var item in updateItems)
                {
                    var existingPrice = existingPrices.First(p => p.Id == item.Id!.Value);
                    if (item.Weight <= 0)
                    {
                        priceIdsToDelete.Add(item.Id!.Value);
                    }
                    else
                    {
                        existingPrice.Weight = item.Weight;
                        pricesToUpdate.Add(existingPrice);
                    }
                }

                if (pricesToUpdate.Any())
                {
                    updatedPriceIds.AddRange(await companyDomesticPathStructPricesRepository.UpdateCompanyDomesticPathStructPriceAsync(pricesToUpdate, cancellationToken));
                }

                if (priceIdsToDelete.Any())
                {
                    await companyDomesticPathStructPricesRepository.DeleteCompanyDomesticPathStructPricesAsync(priceIdsToDelete, cancellationToken);
                    updatedPriceIds.AddRange(priceIdsToDelete);
                }
            }

            // مدیریت DomesticPathStructPriceMunicipalAreas
            foreach (var item in command.UpdateCompanyDomesticPathStructPriceItems)
            {
                if (item.StructPriceArea == null || item.StructPriceArea.DomesticPathStructPriceMunicipalAreas == null)
                {
                    logger.LogWarning("StructPriceArea or DomesticPathStructPriceMunicipalAreas is null for item with Id={ItemId}", item.Id ?? 0);
                    return ApiResponse<List<int>>.Error(400, $"StructPriceArea یا DomesticPathStructPriceMunicipalAreas برای آیتم با Id={item.Id ?? 0} نمی‌تواند null باشد");
                }

                var municipalItems = item.StructPriceArea.DomesticPathStructPriceMunicipalAreas;
                if (!municipalItems.Any())
                {
                    logger.LogInformation("DomesticPathStructPriceMunicipalAreas is empty for item with Id={ItemId}", item.Id ?? 0);
                    continue;
                }

                // تعیین Id واقعی برای آیتم
                int actualPriceId;
                if (item.Id == 0 || item.Id == null)
                {
                    var index = command.UpdateCompanyDomesticPathStructPriceItems
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

                // جداسازی آیتم‌ها برای حذف، درج و به‌روزرسانی
                var itemsToDelete = municipalItems
                    .Where(a => a.Id > 0 && a.Price == 0)
                    .Select(a => a.Id)
                    .ToList();

                var itemsToInsert = municipalItems
                    .Where(a => a.Id == 0 && a.Price != 0)
                    .ToList();

                var itemsToUpdate = municipalItems
                    .Where(a => a.Id > 0 && a.Price != 0)
                    .ToList();

                // حذف آیتم‌ها
                if (itemsToDelete.Any())
                {
                    logger.LogInformation("Deleting MunicipalAreas items with IDs: {Ids}", string.Join(",", itemsToDelete));
                    await companyDomesticPathStructPriceMunicipalAreasRepository.Delete(itemsToDelete, cancellationToken);
                }

                // درج آیتم‌های جدید
                if (itemsToInsert.Any())
                {
                    var newMunicipalItems = mapper.Map<List<CompanyDomesticPathStructPriceMunicipalArea>>(itemsToInsert);
                    foreach (var newItem in newMunicipalItems)
                    {
                        newItem.CompanyDomesticPathStructPriceId = actualPriceId;
                    }
                    logger.LogInformation("Inserting {Count} MunicipalAreas items for item with Id={ItemId}", newMunicipalItems.Count, actualPriceId);
                    var insertedMunicipalIds = await companyDomesticPathStructPriceMunicipalAreasRepository.Create(newMunicipalItems, cancellationToken);
                }

                // به‌روزرسانی آیتم‌های موجود
                if (itemsToUpdate.Any())
                {
                    var updateMunicipalItems = mapper.Map<List<CompanyDomesticPathStructPriceMunicipalArea>>(itemsToUpdate);
                    foreach (var updateItem in updateMunicipalItems)
                    {
                        updateItem.CompanyDomesticPathStructPriceId = actualPriceId;
                    }
                    logger.LogInformation("Updating {Count} MunicipalAreas items for item with Id={ItemId}", updateMunicipalItems.Count, actualPriceId);
                    var updatedMunicipalIds = await companyDomesticPathStructPriceMunicipalAreasRepository.Update(updateMunicipalItems, cancellationToken);
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

    public async Task<ApiResponse<int>> CreateCompanyDomesticPathStructPriceAsync(CreateCompanyDomesticPathStructPriceCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyDomesticPathStructPrice is Called with {@CreateCompanyDomesticPathStructPriceCommand}", command);

        if (command == null)
            return ApiResponse<int>.Error(400, "ورودی ایجاد قیمت ساختار مسیر نمی‌تواند null باشد");

        var exist = await companyDomesticPathStructPricesRepository.CheckExistPrice(
            command.CompanyDomesticPathId, command.Weight, command.PathStructType, command.WeightType, cancellationToken);

        if (exist)
            return ApiResponse<int>.Error(409, "قیمت ساختار مشابه وجود دارد");

        var price = mapper.Map<CompanyDomesticPathStructPrice>(command);
        if (price == null)
            return ApiResponse<int>.Error(500, "مشکل در عملیات تبدیل");

        var priceId = await companyDomesticPathStructPricesRepository.CreateCompanyDomesticPathStructPriceAsync(price, cancellationToken);
        logger.LogInformation("CompanyDomesticPathStructPrice created successfully ReubenId: {PriceId}", priceId);
        return ApiResponse<int>.Created(priceId, "CompanyDomesticPathStructPrice created successfully");
    }


    public async Task<ApiResponse<List<TableDataDto>>> GetTableDataAsync(GetCompanyDomesticPathStructPriceTableDataQuery query, CancellationToken cancellationToken)
    {
        if (query.PathStruct <= 0 || query.CompanyDomesticPathId <= 0 || query.BaseCost > 1 || query.BaseCost < 0)
            return ApiResponse<List<TableDataDto>>.Error(400, "مسیر وجود ندارد یا شناسه, و اطلاعات ارسالی ان اشتباه است");


        var domesticPth = await domesticPathsRepository.GetCompanyDomesticPathByIdAsync(query.CompanyDomesticPathId, false, false, false, false, cancellationToken);
        if (domesticPth is null)
            return ApiResponse<List<TableDataDto>>.Error(400, "مسیر وجود ندارد یا شناسه ان اشتباه است");


        var (items, totalCount) = await companyDomesticPathStructPricesRepository.GetAllCompanyDomesticPathStructPrice(
    "", query.CompanyDomesticPathId, query.PathStruct, 15, 1,
     null, SortDirection.Ascending, cancellationToken);


        IReadOnlyList<Area> municipalAreas = new List<Area>();
        if (query.BaseCost == 0)
        {

            // municipalAreas = await areaRepository.GetDistrictAreasByCityIdAsync(domesticPth.SourceCityId, cancellationToken);
        }
        if (query.BaseCost == 1)
        {
            //municipalAreas = await areaRepository.GetDistrictAreasByCityIdAsync(domesticPth.DestinationCityId, cancellationToken);

        }

        var weightTypes = identityService.GetWeightTypeList(new List<int>() { 0, 1 });


        var tableData = new List<TableDataDto>();

        // اضافه کردن ردیف بدون نام
        var unnamedRow = new TableDataDto
        {
            MunicipalAreaId = 0,
            MunicipalAreaPesrsianName = "-",
            PathStructType = query.PathStruct,
            CompanyDomesticPathId = query.CompanyDomesticPathId, // پیش‌فرض
            Id = 0
        };
        tableData.Add(unnamedRow);

        // اضافه کردن ردیف‌های مناطق
        foreach (var area in municipalAreas)
        {
            tableData.Add(new TableDataDto
            {
                MunicipalAreaId = area.Id,
                MunicipalAreaPesrsianName = area.PersianName ?? "-*-",
                PathStructType = query.PathStruct,
                CompanyDomesticPathId = query.CompanyDomesticPathId,
                Id = 0
            });
        }

        // پر کردن داده‌های موجود
        foreach (var sub in items)
        {

            // ردیف بدون نام
            if (sub.MunicipalAreaId == 0)
            {
                var row = tableData.First(r => r.MunicipalAreaId == 0);
                row.Fields[(int)sub.WeightType] = new FieldDataDto
                {
                    Weight = sub.Weight,
                    Price = null,
                    Static = false,
                    Id = sub.Id
                };
            }

            // ردیف‌های مناطق
            foreach (var municipal in sub.CompanyDomesticPathStructPriceMunicipalAreas ?? [])
            {
                var row = tableData.FirstOrDefault(r => r.MunicipalAreaId == municipal.MunicipalAreaId);
                if (row != null)
                {
                    row.Fields[(int)municipal.WeightType] = new FieldDataDto
                    {
                        Weight = null,
                        Price = municipal.Price,
                        Static = municipal.Static,
                        Id = municipal.Id,
                        CompanyDomesticPathStructPriceId = municipal.CompanyDomesticPathStructPriceId

                    };
                }
            }
        }

        // پر کردن فیلدهای خالی برای همه ردیف‌ها
        foreach (var row in tableData)
        {
            foreach (var weightType in weightTypes.Data!.Items.ToList())
            {
                if (!row.Fields.ContainsKey(weightType.Value))
                {
                    row.Fields[weightType.Value] = new FieldDataDto
                    {
                        Weight = null,
                        Price = null,
                        Static = false,
                        Id = 0
                    };
                }
            }
        }

        var data = tableData.OrderBy(r => r.MunicipalAreaId).ToList();

        return ApiResponse<List<TableDataDto>>.Ok(data, "DomesticPathStructType Data Generated Successfully ");

    }


    public async Task SaveTableDataAsync(List<TableDataDto> tableData, CancellationToken cancellationToken)
    {




        //    public record UpdateCompanyDomesticPathStructPriceItem
        //{
        //    public int Weight { get; set; }
        //    public WeightType WeightType { get; set; }
        //    public int? Id { get; set; }
        //    public int CompanyDomesticPathId { get; set; }
        //    public PathStructType PathStructType { get; set; }
        //    public UpdateCompanyDomesticPathStructPriceMunicipalAreasCommand? StructPriceArea { get; set; } = new();

        //}

        var entityList = new List<UpdateCompanyDomesticPathStructPriceItem>();



        foreach (var row in tableData)
        {

            var itemEntity = new UpdateCompanyDomesticPathStructPriceItem
            {
                CompanyDomesticPathId = row.CompanyDomesticPathId,
                PathStructType = (PathStructType)row.PathStructType,
                Id = row.Id,


            };

            foreach (var field in row.Fields)
            {

                var weightType = field.Key;
                var fieldData = field.Value;

                itemEntity.WeightType = (WeightType)weightType;
                itemEntity.Weight = fieldData.Weight ?? 0;

                itemEntity.StructPriceArea = new UpdateCompanyDomesticPathStructPriceMunicipalAreasCommand()
                {
                    DomesticPathStructPriceMunicipalAreas = new List<UpdateCompanyDomesticPathStructPriceMunicipalAreasItem>()
                    {

                        new UpdateCompanyDomesticPathStructPriceMunicipalAreasItem
                        {
                            MunicipalAreaId = row.MunicipalAreaId,
                            PathStructType = (PathStructType)row.PathStructType,
                            WeightType = (WeightType)weightType,
                            CompanyDomesticPathId =row.CompanyDomesticPathId,
                            Id = fieldData.Id,
                            Static = fieldData.Static,
                            Price = fieldData.Price ?? 0,
                            CompanyDomesticPathStructPriceId = fieldData.CompanyDomesticPathStructPriceId

                        }


                    }

                };


            }
            entityList.Add(itemEntity);
        }

    }

    public void ConvertTableDataToVerticalModel(List<TableDataDto> tableData, List<WeightTypeDto> weightTypes)
    {



        var entityList = new List<UpdateCompanyDomesticPathStructPriceItem>();





        foreach (var weightType in weightTypes.OrderBy(w => w.Value))
        {
            int weightValue = weightType.Value;


            foreach (var row in tableData.OrderBy(r => r.MunicipalAreaId))
            {

                if (row.Fields.TryGetValue(weightValue, out var fieldData))
                {
                    var entity = new UpdateCompanyDomesticPathStructPriceItem()
                    {

                        Id = fieldData.Id,
                        CompanyDomesticPathId = row.CompanyDomesticPathId,
                        PathStructType = (PathStructType)row.PathStructType,
                        WeightType = (WeightType)weightValue,
                        Weight = row.MunicipalAreaId == 0 ? fieldData.Weight ?? 0 : 0,
                        StructPriceArea = row.MunicipalAreaId != 0 ? new UpdateCompanyDomesticPathStructPriceMunicipalAreasCommand()
                        {

                            DomesticPathStructPriceMunicipalAreas = new List<UpdateCompanyDomesticPathStructPriceMunicipalAreasItem>()
                                {
                                    new UpdateCompanyDomesticPathStructPriceMunicipalAreasItem()
                                    {


                                        Id = fieldData.Id,
                                        MunicipalAreaId = row.MunicipalAreaId,
                                        WeightType = (WeightType)weightValue,
                                        Price = fieldData.Price ?? 0,
                                        Static = fieldData.Static



                                    }


                                }




                        } :
                            new UpdateCompanyDomesticPathStructPriceMunicipalAreasCommand()
                    }
                    ;




                    entityList.Add(entity);
                }
            }
        }


    }

















}











