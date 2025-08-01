using AutoMapper;
using Capitan360.Application.Common;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.CreateCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.DeleteCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Commands.UpdateCompanyInsuranceCharge;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Queries.GetAllCompanyInsuranceCharges;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Queries.GetCompanyInsuranceChargeById;
using Capitan360.Application.Services.CompanyServices.CompanyInsurance.Dtos;
using Capitan360.Application.Services.Identity.Services;
using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Constants;
using Capitan360.Domain.Entities.CompanyEntity;
using Capitan360.Domain.Repositories.CompanyRepo;
using Capitan360.Domain.Repositories.ContentRepo;
using Microsoft.Extensions.Logging;

namespace Capitan360.Application.Services.CompanyServices.CompanyInsurance.CompanyInsuranceCharge.Services;

public class CompanyInsuranceChargeService(
    ILogger<CompanyInsuranceChargeService> logger,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ICompanyInsuranceChargeRepository companyInsuranceChargeRepository,
    ICompanyInsuranceChargePaymentRepository chargePaymentRepository,
    ICompanyInsuranceChargePaymentContentTypeRepository chargePaymentContentTypeRepository,
    ICompanyInsuranceRepository companyInsuranceRepository,
    ICompanyContentTypeRepository companyContentTypeRepository,
    IIdentityService identityService)
    : ICompanyInsuranceChargeService
{
    public async Task<ApiResponse<List<int>>> CreateCompanyInsuranceChargeAsync(CreateCompanyInsuranceChargeListCommand? command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCompanyInsuranceCharge is Called with {@CreateCompanyInsuranceChargeCommand}", command);

        if (command is null || !command.CompanyInsuranceChargeList.Any())
            return ApiResponse<List<int>>.Error(400, "ورودی ایجاد شارژ بیمه شرکت نمی‌تواند null باشد");

        if (command.CompanyInsuranceChargeList.Any(a => a.Settlement < 0 || a.Value < 0 || a.CompanyInsuranceId <= 0))
        {
            return ApiResponse<List<int>>.Error(400, "لیست شناسه مسیر  نمی‌تواند null یا خالی باشد");
        }

        if (command.CompanyInsuranceChargeList.Any(a => a.Settlement == 0 || a.Value == 0))
        {
            foreach (var item in command.CompanyInsuranceChargeList.ToList())
            {
                if (item.Settlement == 0 || item.Value == 0)
                {
                    command.CompanyInsuranceChargeList.Remove(item);
                }
            }
        }
        if (!command.CompanyInsuranceChargeList.Any())
            return ApiResponse<List<int>>.Error(400, "ورودی ایجاد شارژ بیمه شرکت نمی‌تواند null باشد");

        var subOneItems = command.CompanyInsuranceChargeList.Select(a => a.SubOneChargePaymentCommand).ToList();
        var subTwoItems = command.CompanyInsuranceChargeList.Select(a => a.SubTwoChargePaymentContentTypeCommand).ToList();

        foreach (var item in subOneItems)
        {
            if (item.Any(a => a.Diff < 0))
                return ApiResponse<List<int>>.Error(400, "لیست شناسه مسیر   SubOneChargePaymentCommandنمی‌تواند null یا خالی باشد");
            if (item.Any(a => a.Diff == 0))
                item.RemoveAll(a => a.Diff == 0);
        }
        foreach (var item in subTwoItems)
        {
            if (item.Any(a => a.RateDiff < 0 || a.RateSettlement < 0))
                return ApiResponse<List<int>>.Error(400, "لیست شناسه مسیر   SubOneChargePaymentCommandنمی‌تواند null یا خالی باشد");

            if (item.Any(a => a is { RateDiff: 0, RateSettlement: 0 }))
                item.RemoveAll(a => a is { RateDiff: 0, RateSettlement: 0 });
        }

        //if (subOneItems[0].Any(a => a.Diff < 0))
        //    return ApiResponse<List<int>>.Error(400, "لیست شناسه مسیر   SubOneChargePaymentCommandنمی‌تواند null یا خالی باشد");

        //if (subTwoItems.Any(a => a.RateDiff < 0 || a.RateSettlement < 0))
        //    return ApiResponse<List<int>>.Error(400, "لیست شناسه مسیر   SubOneChargePaymentCommandنمی‌تواند null یا خالی باشد");

        //if (subOneItems.Any(a => a.Diff == 0))
        //    subOneItems.RemoveAll(a => a.Diff == 0);

        //if (subTwoItems.Any(a => a is { RateDiff: 0, RateSettlement: 0 }))
        //    subTwoItems.RemoveAll(a => a is { RateDiff: 0, RateSettlement: 0 });

        var companyInsuranceEntities = mapper.Map<List<Domain.Entities.CompanyEntity.CompanyInsuranceCharge>>(command.CompanyInsuranceChargeList);
        if (companyInsuranceEntities == null || !companyInsuranceEntities.Any())
            return ApiResponse<List<int>>.Error(500, "مشکل در عملیات تبدیل");

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var insertedCompanyInsuranceChargeId = await companyInsuranceChargeRepository.CreateCompanyInsuranceChargeListAsync(companyInsuranceEntities, cancellationToken);

        if (insertedCompanyInsuranceChargeId.Count <= 0 || !insertedCompanyInsuranceChargeId.Any())
            return ApiResponse<List<int>>.Error(500, "خطا در ذخیره‌سازی CompanyInsuranceCharge");
        logger.LogInformation("CompanyInsuranceCharge created successfully with IDs: {CompanyInsuranceChargeIds}", string.Join(",", insertedCompanyInsuranceChargeId));

        var newSubOneItems = new List<CompanyInsuranceChargePayment>();
        var newSubTwoItems = new List<CompanyInsuranceChargePaymentContentType>();

        for (var i = 0; i < command.CompanyInsuranceChargeList.Count; i++)
        {
            // var commandItem = command.CompanyInsuranceChargeList[i];

            var insertedCompanyChargeId = insertedCompanyInsuranceChargeId[i];

            var subOneItem = subOneItems[i];
            var subTwoItem = subTwoItems[i];

            if (subOneItem.Any())
            {
                var mappedItems = mapper.Map<List<CompanyInsuranceChargePayment>>(subOneItem);
                foreach (var mappedItem in mappedItems)
                {
                    mappedItem.CompanyInsuranceChargeId = insertedCompanyChargeId;
                    newSubOneItems.Add(mappedItem);
                }
            }

            if (subTwoItem.Any())
            {
                var mappedItems = mapper.Map<List<CompanyInsuranceChargePaymentContentType>>(subTwoItem);
                foreach (var mappedItem in mappedItems)
                {
                    mappedItem.CompanyInsuranceChargeId = insertedCompanyChargeId;
                    newSubTwoItems.Add(mappedItem);
                }
            }

            #region MyRegion

            //var commandItem = command.CompanyInsuranceChargeList[i];
            //var insertedCompanyChargeId = insertedCompanyInsuranceChargeId[i];

            //if (commandItem.SubOneChargePaymentCommand.Any())
            //{
            //    var mappedItems = mapper.Map<List<CompanyInsuranceChargePayment>>(commandItem.SubOneChargePaymentCommand);

            //    foreach (var mappedItem in mappedItems)
            //    {
            //        mappedItem.CompanyInsuranceChargeId = insertedCompanyChargeId;
            //        newSubOneItems.Add(mappedItem);

            //    }
            //}
            //if (commandItem.SubTwoChargePaymentContentTypeCommand.Any())
            //{
            //    var mappedItems = mapper.Map<List<CompanyInsuranceChargePaymentContentType>>(commandItem.SubTwoChargePaymentContentTypeCommand);

            //    foreach (var mappedItem in mappedItems)
            //    {
            //        mappedItem.CompanyInsuranceChargeId = insertedCompanyChargeId;
            //        newSubTwoItems.Add(mappedItem);

            //    }
            //}

            #endregion MyRegion
        }

        if (newSubOneItems.Any())
        {
            var chargePaymentIds = await chargePaymentRepository.CreateInsuranceChargePayment(newSubOneItems, cancellationToken);
            logger.LogInformation("created successfully with IDs: {MunicipalAreaIds}", string.Join(",", chargePaymentIds));
        }

        if (newSubTwoItems.Any())
        {
            var chargePaymentContentTypeIds = await chargePaymentContentTypeRepository.CreateInsuranceChargePayment(newSubTwoItems, cancellationToken);
            logger.LogInformation("created successfully with IDs: {MunicipalAreaIds}", string.Join(",", chargePaymentContentTypeIds));
        }

        await unitOfWork.CommitTransactionAsync(cancellationToken);
        return ApiResponse<List<int>>.Created(insertedCompanyInsuranceChargeId, "CompanyDomesticPathStructPrices و MunicipalAreas با موفقیت ایجاد شدند");
    }

    public async Task<ApiResponse<PagedResult<CompanyInsuranceChargeDto>>> GetAllCompanyInsuranceCharges(GetAllCompanyInsuranceChargesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetAllCompanyInsuranceCharges is Called");

        if (query.PageSize <= 0 || query.PageNumber <= 0)
            return ApiResponse<PagedResult<CompanyInsuranceChargeDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        var (companyInsuranceCharges, totalCount) = await companyInsuranceChargeRepository.GetMatchingAllCompanyInsuranceCharges(
            query.SearchPhrase,
            query.CompanyInsuranceId,
            query.PageSize,
            query.PageNumber,
            query.SortBy,
            query.SortDirection,
            cancellationToken);

        var companyInsuranceChargeDto = mapper.Map<IReadOnlyList<CompanyInsuranceChargeDto>>(companyInsuranceCharges)
                                        ?? Array.Empty<CompanyInsuranceChargeDto>();
        logger.LogInformation("Retrieved {Count} company insurance charges", companyInsuranceChargeDto.Count);

        var data = new PagedResult<CompanyInsuranceChargeDto>(companyInsuranceChargeDto, totalCount, query.PageSize, query.PageNumber);
        return ApiResponse<PagedResult<CompanyInsuranceChargeDto>>.Ok(data, "CompanyInsuranceCharges retrieved successfully");

        //logger.LogInformation("GetAllCompanyDomesticPathStructQuery is Called");
        //if (query.PageSize <= 0 || query.PageNumber <= 0)
        //    return ApiResponse<PagedResult<CompanyDomesticPathStructPriceDto>>.Error(400, "اندازه صفحه یا شماره صفحه نامعتبر است");

        //var (items, totalCount) = await companyDomesticPathStructPricesRepository.GetMatchingAllCompanyDomesticPathStructPrice(
        //    query.SearchPhrase, query.CompanyDomesticPathId, query.PathStruct, query.PageSize, query.PageNumber,
        //    query.SortBy, query.SortDirection, cancellationToken);

        //var priceDtos = mapper.Map<IReadOnlyList<CompanyDomesticPathStructPriceDto>>(items) ?? Array.Empty<CompanyDomesticPathStructPriceDto>();

        //logger.LogInformation("Retrieved {Count} company domestic path struct items", priceDtos.Count);

        //var data = new PagedResult<CompanyDomesticPathStructPriceDto>(priceDtos, totalCount, query.PageSize, query.PageNumber);
        //return ApiResponse<PagedResult<CompanyDomesticPathStructPriceDto>>.Ok(data, "Company domestic path struct items retrieved successfully");
    }

    public async Task<ApiResponse<CompanyInsuranceChargeDto>> GetCompanyInsuranceChargeByIdAsync(GetCompanyInsuranceChargeByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetCompanyInsuranceChargeById is Called with ID: {Id}", query.Id);

        if (query.Id <= 0)
            return ApiResponse<CompanyInsuranceChargeDto>.Error(400, "شناسه شارژ بیمه شرکت باید بزرگ‌تر از صفر باشد");

        var companyInsuranceCharge = await companyInsuranceChargeRepository.GetCompanyInsuranceChargeById(query.Id, cancellationToken);
        if (companyInsuranceCharge == null)
            return ApiResponse<CompanyInsuranceChargeDto>.Error(404, $"شارژ بیمه شرکت با شناسه {query.Id} یافت نشد");

        var result = mapper.Map<CompanyInsuranceChargeDto>(companyInsuranceCharge);
        logger.LogInformation("CompanyInsuranceCharge retrieved successfully with ID: {Id}", query.Id);
        return ApiResponse<CompanyInsuranceChargeDto>.Ok(result, "CompanyInsuranceCharge retrieved successfully");
    }

    public async Task<ApiResponse<object>> DeleteCompanyInsuranceChargeAsync(DeleteCompanyInsuranceChargeCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteCompanyInsuranceCharge is Called with ID: {Id}", command.Id);

        if (command.Id <= 0)
            return ApiResponse<object>.Error(400, "شناسه شارژ بیمه شرکت باید بزرگ‌تر از صفر باشد");

        var companyInsuranceCharge = await companyInsuranceChargeRepository.GetCompanyInsuranceChargeById(command.Id, cancellationToken);
        if (companyInsuranceCharge == null)
            return ApiResponse<object>.Error(404, $"شارژ بیمه شرکت با شناسه {command.Id} یافت نشد");

        companyInsuranceChargeRepository.Delete(companyInsuranceCharge);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("CompanyInsuranceCharge soft-deleted successfully with ID: {Id}", command.Id);
        return ApiResponse<object>.Deleted("CompanyInsuranceCharge deleted successfully");
    }

    public async Task<ApiResponse<List<int>>> UpdateCompanyInsuranceChargeAsync(UpdateCompanyInsuranceChargeListCommand? command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateCompanyInsuranceCharge is Called with {@UpdateCompanyInsuranceChargeCommand}", command);

        if (command == null || !command.CompanyInsuranceChargeList.Any())
            return ApiResponse<List<int>>.Error(400, "ورودی ایجاد شارژ بیمه شرکت نمی‌تواند null باشد");

        var (mainUpdateList, mainInsertList, mainDeleteList) = ProcessMainItems(command.CompanyInsuranceChargeList);

        await unitOfWork.BeginTransactionAsync(cancellationToken);
        var updatedIds = new List<int>();

        // Handle updateMainList
        if (mainUpdateList.Any())
        {
            var existedMainItems =
                await companyInsuranceChargeRepository.GetExistingCompanyInsuranceCharge(
                    mainUpdateList.Select(a => a.Id).ToList(), cancellationToken);

            var mainItemsShouldBeUpdated = new List<Domain.Entities.CompanyEntity.CompanyInsuranceCharge>();

            foreach (var item in mainUpdateList)
            {
                var existedMainItem = existedMainItems.First(x => x.Id == item.Id);

                existedMainItem.Settlement = item.Settlement;
                existedMainItem.Value = item.Value;
                existedMainItem.IsPercent = item.IsPercent;
                existedMainItem.Static = item.Static;
                mainItemsShouldBeUpdated.Add(existedMainItem);
            }

            if (mainItemsShouldBeUpdated.Any())
                updatedIds.AddRange(await companyInsuranceChargeRepository.UpdateByListAsync(mainItemsShouldBeUpdated, cancellationToken));
        }

        // Handle Delete

        if (mainDeleteList.Any())
        {
            var mainItemsShouldBeDeleted = mainDeleteList.Select(a => a.Id).ToList();

            await companyInsuranceChargeRepository.DeleteByListAsync(mainItemsShouldBeDeleted, cancellationToken);

            // deleted References

            var subOneReferenceIds =
                await chargePaymentRepository.ChargePaymentReferenceIds(mainItemsShouldBeDeleted, cancellationToken);
            await chargePaymentRepository.Delete(subOneReferenceIds, cancellationToken);

            var subTwoReferenceIds =
                await chargePaymentContentTypeRepository.ChargePaymentContentTypeReferenceIds(mainItemsShouldBeDeleted, cancellationToken);
            await chargePaymentContentTypeRepository.Delete(subTwoReferenceIds, cancellationToken);

            updatedIds.AddRange(mainItemsShouldBeDeleted);
        }

        // Handle Insert
        var newIdsMap = new Dictionary<int, int>();

        if (mainInsertList.Any())
        {
            var newMainItems = mapper.Map<List<Domain.Entities.CompanyEntity.CompanyInsuranceCharge>>(mainInsertList);

            for (int i = 0; i < newMainItems.Count; i++)
            {
                newIdsMap.Add(i, -1);
            }

            var insertedNewMainItems =
               await companyInsuranceChargeRepository.CreateCompanyInsuranceChargeListAsync(newMainItems, cancellationToken);
            updatedIds.AddRange(insertedNewMainItems);

            for (int i = 0; i < insertedNewMainItems.Count; i++)
            {
                newIdsMap[i] = insertedNewMainItems[i];
            }
        }

        // SubItem one

        foreach (var item in command.CompanyInsuranceChargeList)
        {
            var (subOneUpdateList, subOneInsertList, subOneDeleteList) = ProcessSubOneItems(item.SubOneChargePaymentCommand);

            var (subTwoUpdateList, subTwoInsertList, subTwoDeleteList) = ProcessSubTwoItems(item.SubTwoChargePaymentContentTypeCommand);

            // HandleDelete
            if (subOneDeleteList.Any())
            {
                var shouldBeDeletedSubOneIds = subOneDeleteList.Select(a => a.Id).ToList();
                logger.LogInformation("Deleting  items with IDs: {Ids}", string.Join(",", shouldBeDeletedSubOneIds));
                await chargePaymentRepository.Delete(shouldBeDeletedSubOneIds, cancellationToken);
            }

            if (subTwoDeleteList.Any())
            {
                var shouldBeDeletedSubTwoIds = subTwoDeleteList.Select(a => a.Id).ToList();

                logger.LogInformation("Deleting  items with IDs: {Ids}", string.Join(",", shouldBeDeletedSubTwoIds));
                await chargePaymentContentTypeRepository.Delete(shouldBeDeletedSubTwoIds, cancellationToken);
            }

            // Getting Id
            int actualCompanyInsuranceChargeId;

            //TODO : Check This logic
            if (item.Id == 0 && (subOneInsertList.Count > 0 || subTwoInsertList.Count > 0))
            {
                var index = command.CompanyInsuranceChargeList
                    .Where(a => a.Id == 0)
                    .ToList()
                    .IndexOf(item);
                if (!newIdsMap.ContainsKey(index))
                {
                    logger.LogError("No NewInsertId found for new item at index {Index}", index);

                    return ApiResponse<List<int>>.Error(400, $"No NewInsertId found for new item at index {index}");
                }

                actualCompanyInsuranceChargeId = newIdsMap[index];
            }
            else
            {
                actualCompanyInsuranceChargeId = item.Id;
            }

            // Handle Update

            if (subOneUpdateList.Any())
            {
                var shouldBeUpdatedSubOne = mapper.Map<List<CompanyInsuranceChargePayment>>(subOneUpdateList);
                foreach (var updateItem in shouldBeUpdatedSubOne)
                {
                    updateItem.CompanyInsuranceChargeId = actualCompanyInsuranceChargeId;
                }
                logger.LogInformation("Updating {Count}  items for item with Id={ItemId}",
                    shouldBeUpdatedSubOne.Count, actualCompanyInsuranceChargeId);
                await chargePaymentRepository.Update(shouldBeUpdatedSubOne, cancellationToken);
            }

            if (subTwoUpdateList.Any())
            {
                var shouldBeUpdatedSubTwo = mapper.Map<List<CompanyInsuranceChargePaymentContentType>>(subTwoUpdateList);
                foreach (var updateItem in shouldBeUpdatedSubTwo)
                {
                    updateItem.CompanyInsuranceChargeId = actualCompanyInsuranceChargeId;
                }
                logger.LogInformation("Updating {Count}  items for item with Id={ItemId}",
                    shouldBeUpdatedSubTwo.Count, actualCompanyInsuranceChargeId);
                await chargePaymentContentTypeRepository.Update(shouldBeUpdatedSubTwo, cancellationToken);
            }

            // Handle Insert

            if (subOneInsertList.Any())
            {
                var shouldBeInsertedSubOne = mapper.Map<List<CompanyInsuranceChargePayment>>(subOneInsertList);
                foreach (var insertItem in shouldBeInsertedSubOne)
                {
                    insertItem.CompanyInsuranceChargeId = actualCompanyInsuranceChargeId;
                }
                logger.LogInformation("Updating {Count}  items for item with Id={ItemId}",
    shouldBeInsertedSubOne.Count, actualCompanyInsuranceChargeId);
                await chargePaymentRepository.CreateInsuranceChargePayment(shouldBeInsertedSubOne, cancellationToken);
            }

            if (subTwoInsertList.Any())
            {
                var shouldBeInsertedSubTwo = mapper.Map<List<CompanyInsuranceChargePaymentContentType>>(subTwoInsertList);
                foreach (var insertItem in shouldBeInsertedSubTwo)
                {
                    insertItem.CompanyInsuranceChargeId = actualCompanyInsuranceChargeId;
                }
                logger.LogInformation("Updating {Count}  items for item with Id={ItemId}",
                    shouldBeInsertedSubTwo.Count, actualCompanyInsuranceChargeId);
                await chargePaymentContentTypeRepository.CreateInsuranceChargePayment(shouldBeInsertedSubTwo, cancellationToken);
            }
        }

        await unitOfWork.CommitTransactionAsync(cancellationToken);

        logger.LogInformation("CompanyInsuranceCharge and CompanyInsuranceChargePayment And ContentType updated/inserted/deleted successfully with IDs: {PriceIds}", string.Join(",", updatedIds));
        return ApiResponse<List<int>>.Ok(updatedIds, "CompanyInsuranceCharge و CompanyInsuranceChargePayment و ContentType  با موفقیت به‌روزرسانی/درج/حذف شدند");
    }

    // UpdateCompanyInsuranceChargePaymentCommand

    //Helpers

    public async Task<ApiResponse<List<CompanyInsuranceChargeTableDataDto>>> GetTableDataAAsync(GetCompanyInsuranceChargeTableDataQuery query, CancellationToken cancellationToken)
    {
        if (query.CompanyInsuranceId <= 0)
            return ApiResponse<List<CompanyInsuranceChargeTableDataDto>>.Error(400, "شرکت بیمه وجود ندارد یا شناسه, و اطلاعات ارسالی ان اشتباه است");

        var companyInsurance = await companyInsuranceRepository.GetCompanyInsuranceById(query.CompanyInsuranceId, cancellationToken);
        if (companyInsurance is null)
            return ApiResponse<List<CompanyInsuranceChargeTableDataDto>>.Error(400, "شرکت بیمه وجود ندارد یا شناسه ان اشتباه است");

        var (items, totalCount) = await companyInsuranceChargeRepository.GetMatchingAllCompanyInsuranceCharges(
    "", query.CompanyInsuranceId, 100, 1,
     null, SortDirection.Ascending, cancellationToken);

        var (contentTypesData, total) = await companyContentTypeRepository
            .GetCompanyContentTypes("", companyInsurance.CompanyId, 1, 100, 1, null, SortDirection.Ascending, cancellationToken);

        var rates = identityService.GetRateList();

        var tableData = new List<CompanyInsuranceChargeTableDataDto>();

        //    // اضافه کردن ردیف بدون نام
        var unnamedCharge = new CompanyInsuranceChargeTableDataDto
        {
            ContentTypeId = -1,
            RowName = "نرخنامه",
            CompanyInsuranceId = query.CompanyInsuranceId, // پیش‌فرض

            Id = 0
        };
        var unnamedRow = new CompanyInsuranceChargeTableDataDto
        {
            ContentTypeId = 0,
            RowName = "-",
            CompanyInsuranceId = query.CompanyInsuranceId, // پیش‌فرض

            Id = 0
        };

        tableData.Add(unnamedCharge);
        tableData.Add(unnamedRow);

        //    // اضافه کردن ردیف‌های مناطق
        foreach (var content in contentTypesData)
        {
            tableData.Add(new CompanyInsuranceChargeTableDataDto
            {
                ContentTypeId = content.ContentTypeId,
                ContentTypeName = content.ContentTypeName ?? "-*-",

                Id = 0
            });
        }

        //    // پر کردن داده‌های موجود
        foreach (var sub in items)
        {
            // ردیف بدون نام
            //if (sub.ContentTypeId == -1)
            //{
            var rowValue = tableData.First(r => r.ContentTypeId == -1);

            rowValue.Fields[(int)sub.Rate] = new InsuranceChargeFieldDto
            {
                //  Weight = ((int)sub.WeightType == -1 || (int)sub.WeightType == 0) ? null : sub.Weight,
                Rate = (int)sub.Rate,
                Tinsurance = sub.Settlement,
                Value = sub.Value,
                Static = sub.Static,
                Id = sub.Id
            };

            var firstSubChargePayment = sub.CompanyInsuranceChargePayments;

            foreach (var subOne in firstSubChargePayment)
            {
                var rowBenefit = tableData.First(r => r.ContentTypeId == 0);

                rowBenefit.Fields[(int)sub.Rate] = new InsuranceChargeFieldDto
                {
                    Rate = (int)sub.Rate,
                    Tinsurance = sub.Settlement,
                    Diff = subOne.Diff,
                    Id = subOne.Id,
                    CompanyInsuranceChargeId = subOne.CompanyInsuranceChargeId
                };
            }

            //   }

            //        // ردیف‌های مناطق
            foreach (var subTwo in sub.CompanyInsuranceChargePaymentContentTypes ?? [])
            {
                var subTwoRow = tableData.First(r => r.ContentTypeId == subTwo.ContentId);

                subTwoRow.Fields[(int)sub.Rate] = new InsuranceChargeFieldDto
                {
                    Id = subTwo.Id,
                    ContentTypeId = subTwo.ContentId,
                    RateSettlement = subTwo.RateSettlement,
                    RateDiff = subTwo.RateDiff,
                    Rate = (int)sub.Rate,
                    CompanyInsuranceChargeId = subTwo.CompanyInsuranceChargeId
                };
            }
        }

        //    //پر کردن فیلدهای خالی برای همه ردیف‌ها
        foreach (var row in tableData)
        {
            row.CompanyInsuranceId = row.CompanyInsuranceId;

            foreach (var rate in rates.Data!.Items.ToList())
            {
                if (!row.Fields.ContainsKey(rate.Value))
                {
                    row.Fields[rate.Value] = new InsuranceChargeFieldDto
                    {
                        Static = false,
                        Id = 0,
                        ContentTypeId = row.ContentTypeId,

                        Diff = 0,
                        Rate = rate.Value,
                        Tinsurance = 0,
                        RateDiff = 0,
                        RateSettlement = 0,
                        Value = 0
                    };
                }
            }
        }

        var data = tableData.OrderBy(r => r.ContentTypeId).ToList();

        return ApiResponse<List<CompanyInsuranceChargeTableDataDto>>.Ok(data, "DomesticPathStructType Data Generated Successfully ");
    }

    public class CompanyInsuranceChargeTableDataDto
    {
        public int Id { get; set; } = 0;
        public int ContentTypeId { get; set; } = 0;
        public string ContentTypeName { get; set; }
        public string RowName { get; set; }
        public int? CompanyInsuranceId { get; set; } = 0;
        public int? CompanyInsuranceChargeId { get; set; } = 0;

        public Dictionary<int, InsuranceChargeFieldDto> Fields { get; set; } = new();
    }

    public class InsuranceChargeFieldDto
    {
        public int? Rate { get; set; } // برای بدون نام
        public decimal? Value { get; set; }  // برای مناطق
        public decimal? Tinsurance { get; set; }   // برای مناطق
        public bool Static { get; set; }
        public int Id { get; set; } = 0;

        public int ContentTypeId { get; set; } = 0;
        public int? CompanyInsuranceChargeId { get; set; } = 0;
        public decimal? Diff { get; set; }
        public decimal? RateSettlement { get; set; }
        public decimal? RateDiff { get; set; }
    }

    public record GetCompanyInsuranceChargeTableDataQuery(int CompanyInsuranceId = 0);

    private (
    List<UpdateCompanyInsuranceChargePaymentCommand> SubOneUpdateList,
    List<UpdateCompanyInsuranceChargePaymentCommand> SubOneInsertList,
    List<UpdateCompanyInsuranceChargePaymentCommand> SubOneDeleteList
    )
    ProcessSubOneItems(List<UpdateCompanyInsuranceChargePaymentCommand> items)
    {
        var subOneUpdateList = new List<UpdateCompanyInsuranceChargePaymentCommand>();
        var subOneInsertList = new List<UpdateCompanyInsuranceChargePaymentCommand>();
        var subOneDeleteList = new List<UpdateCompanyInsuranceChargePaymentCommand>();

        foreach (var item in items)
        {
            // InsertList

            switch (item.Id)
            {
                case <= 0 when item.Diff > 0:
                    subOneInsertList.Add(item);
                    break;

                case > 0 when item.Diff <= 0:
                    subOneDeleteList.Add(item);
                    break;

                case > 0 when item.Diff > 0:
                    subOneUpdateList.Add(item);
                    break;
            }

            #region MyRegion

            //// بررسی شرط نادیده گرفتن وزن
            //bool ignoreWeightCondition = item.WeightType == WeightType.TypeMin || item.WeightType == WeightType.TypeNormal;

            //if (item.Id is 0 or null)
            //{
            //    if (item.Price > 0 && (ignoreWeightCondition || item.Weight > 0))
            //    {
            //        companyDomesticPathChargeInsertList.Add(item);
            //    }

            //}

            //if (item.Id > 0)
            //{
            //    if (item is { Price: <= 0, Weight: <= 0 })
            //    {
            //        companyDomesticPathChargeDeleteList.Add(item);
            //    }
            //    else if (item.Price > 0 && (ignoreWeightCondition || item.Weight > 0))
            //    {
            //        companyDomesticPathChargeUpdateList.Add(item);
            //    }
            //}

            #endregion MyRegion
        }

        return (subOneUpdateList, subOneInsertList, subOneDeleteList);
    }

    private (
List<UpdateCompanyInsuranceChargePaymentContentTypeCommand> SubTwoUpdateList,
List<UpdateCompanyInsuranceChargePaymentContentTypeCommand> SubTwoInsertList,
List<UpdateCompanyInsuranceChargePaymentContentTypeCommand> SubTwoDeleteList
)
ProcessSubTwoItems(List<UpdateCompanyInsuranceChargePaymentContentTypeCommand> items)
    {
        var subTwoUpdateList = new List<UpdateCompanyInsuranceChargePaymentContentTypeCommand>();
        var subTwoInsertList = new List<UpdateCompanyInsuranceChargePaymentContentTypeCommand>();
        var subTwoDeleteList = new List<UpdateCompanyInsuranceChargePaymentContentTypeCommand>();

        foreach (var item in items)
        {
            // InsertList

            switch (item.Id)
            {
                case > 0:
                    {
                        if (item is { RateSettlement: > 0, RateDiff: > 0 })
                        {
                            subTwoUpdateList.Add(item);
                        }

                        if (item.RateSettlement <= 0 || item.RateDiff <= 0)
                        {
                            subTwoDeleteList.Add(item);
                        }

                        break;
                    }
                case <= 0 when item is { RateSettlement: > 0, RateDiff: > 0 }:

                    subTwoInsertList.Add(item);
                    break;
            }
        }

        return (subTwoUpdateList, subTwoInsertList, subTwoDeleteList);
    }

    private (
List<UpdateCompanyInsuranceChargeCommand> MainUpdateList,
List<UpdateCompanyInsuranceChargeCommand> MainInsertList,
List<UpdateCompanyInsuranceChargeCommand> MainDeleteList
)
ProcessMainItems(List<UpdateCompanyInsuranceChargeCommand> items)
    {
        var mainUpdateList = new List<UpdateCompanyInsuranceChargeCommand>();
        var mainInsertList = new List<UpdateCompanyInsuranceChargeCommand>();
        var mainDeleteList = new List<UpdateCompanyInsuranceChargeCommand>();

        foreach (var item in items)
        {
            // InsertList

            switch (item.Id)
            {
                case > 0:
                    {
                        if (item is { Settlement: > 0, Value: > 0 })
                        {
                            mainUpdateList.Add(item);
                        }

                        if (item.Settlement <= 0 || item.Value <= 0)
                        {
                            mainDeleteList.Add(item);
                        }

                        break;
                    }
                case <= 0 when item is { Settlement: > 0, Value: > 0 }:

                    mainInsertList.Add(item);
                    break;
            }
        }

        return (mainUpdateList, mainInsertList, mainDeleteList);
    }
}