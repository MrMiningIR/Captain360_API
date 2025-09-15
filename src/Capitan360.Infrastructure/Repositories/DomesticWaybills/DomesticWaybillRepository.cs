using Capitan360.Application.Utils;
using Capitan360.Domain.Entities.DomesticWaybills;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces;
using Capitan360.Domain.Repositories.DomesticWaybills;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.DomesticWaybills;

public class DomesticWaybillRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IDomesticWaybillRepository
{
    public async Task<DomesticWaybill?> GetDomesticWaybillByIdAsync(int domesticWaybillId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<DomesticWaybill> query = dbContext.DomesticWaybills;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.ManifestForm);
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.DomesticWaybillPackageTypes);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.BikeDeliveryInSenderCompany);
            query = query.Include(item => item.DomesticWaybillPeriod);
            query = query.Include(item => item.CompanySenderBank);
            query = query.Include(item => item.Counter);
            query = query.Include(item => item.CustomerPanel);
            query = query.Include(item => item.InsuranceCompany);
        }

        if (loadDataReceiverCompany)
        {
            query = query.Include(item => item.BikeDeliveryInReceiverCompany);
            query = query.Include(item => item.CompanyReceiverBank);
            query = query.Include(item => item.CompanyReceiverResponsibleCustomer);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.Id == domesticWaybillId, cancellationToken);
    }

    public async Task<DomesticWaybill?> GetDomesticWaybillByNoAsync(long domesticWaybillNo, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<DomesticWaybill> query = dbContext.DomesticWaybills;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.ManifestForm);
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.DomesticWaybillPackageTypes);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.BikeDeliveryInSenderCompany);
            query = query.Include(item => item.DomesticWaybillPeriod);
            query = query.Include(item => item.CompanySenderBank);
            query = query.Include(item => item.Counter);
            query = query.Include(item => item.CustomerPanel);
            query = query.Include(item => item.InsuranceCompany);
        }

        if (loadDataReceiverCompany)
        {
            query = query.Include(item => item.BikeDeliveryInReceiverCompany);
            query = query.Include(item => item.CompanyReceiverBank);
            query = query.Include(item => item.CompanyReceiverResponsibleCustomer);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.No == domesticWaybillNo, cancellationToken);
    }

    public async Task<DomesticWaybill?> GetDomesticWaybillByNoAndCompanySenderIdAsync(long domesticWaybillNo, int companySenderId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<DomesticWaybill> query = dbContext.DomesticWaybills;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.ManifestForm);
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.DomesticWaybillPackageTypes);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.BikeDeliveryInSenderCompany);
            query = query.Include(item => item.DomesticWaybillPeriod);
            query = query.Include(item => item.CompanySenderBank);
            query = query.Include(item => item.Counter);
            query = query.Include(item => item.CustomerPanel);
            query = query.Include(item => item.InsuranceCompany);
        }

        if (loadDataReceiverCompany)
        {
            query = query.Include(item => item.BikeDeliveryInReceiverCompany);
            query = query.Include(item => item.CompanyReceiverBank);
            query = query.Include(item => item.CompanyReceiverResponsibleCustomer);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.No == domesticWaybillNo && item.CompanySenderId == companySenderId, cancellationToken);
    }

    public async Task<DomesticWaybill?> GetDomesticWaybillByNoAndCompanyReceiverIdAsync(long domesticWaybillNo, int companyReceiverId, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, bool tracked, CancellationToken cancellationToken)
    {
        IQueryable<DomesticWaybill> query = dbContext.DomesticWaybills;

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.ManifestForm);
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.DomesticWaybillPackageTypes);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.BikeDeliveryInSenderCompany);
            query = query.Include(item => item.DomesticWaybillPeriod);
            query = query.Include(item => item.CompanySenderBank);
            query = query.Include(item => item.Counter);
            query = query.Include(item => item.CustomerPanel);
            query = query.Include(item => item.InsuranceCompany);
        }

        if (loadDataReceiverCompany)
        {
            query = query.Include(item => item.BikeDeliveryInReceiverCompany);
            query = query.Include(item => item.CompanyReceiverBank);
            query = query.Include(item => item.CompanyReceiverResponsibleCustomer);
        }

        if (!tracked)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(item => item.No == domesticWaybillNo && item.CompanyReceiverId == companyReceiverId, cancellationToken);
    }

    public async Task<bool> CheckExistDomesticWaybillByIdAsync(int domesticWaybillId, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.AnyAsync(item => item.Id == domesticWaybillId, cancellationToken);
    }

    public async Task<bool> CheckExistDomesticWaybillByNoAsync(long domesticWaybillNo, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.AnyAsync(item => item.No == domesticWaybillNo, cancellationToken);
    }

    public async Task<bool> CheckExistDomesticWaybillByNoAndCompanySenderIdAsync(long domesticWaybillNo, int companySenderId, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.AnyAsync(item => item.No == domesticWaybillNo && item.CompanySenderId == companySenderId, cancellationToken);
    }

    public async Task<bool> CheckExistDomesticWaybillByNoAndCompanyReceiverIdAsync(long domesticWaybillNo, int companyReceiverId, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.AnyAsync(item => item.No == domesticWaybillNo && item.CompanyReceiverId == companyReceiverId, cancellationToken);
    }

    public async Task<int> InsertDomesticWaybillAsync(DomesticWaybill domesticWaybill, CancellationToken cancellationToken)
    {
        dbContext.DomesticWaybills.Add(domesticWaybill);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return domesticWaybill.Id;
    }

    public async Task DeleteDomesticWaybillAsync(int domesticWaybillId, CancellationToken cancellationToken)
    {
        var domesticWaybillPackageTypes = await dbContext.DomesticWaybillPackageTypes.Where(item => item.DomesticWaybillId == domesticWaybillId).ToListAsync(cancellationToken);
        if (domesticWaybillPackageTypes == null)
            return;
     
        dbContext.DomesticWaybillPackageTypes.RemoveRange(domesticWaybillPackageTypes);

        var domesticWaybill = await dbContext.DomesticWaybills.SingleOrDefaultAsync(item => item.Id == domesticWaybillId, cancellationToken);
        if (domesticWaybill == null)
            return;

        dbContext.DomesticWaybills.Remove(domesticWaybill);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<int>> GetWaybillIdByManifestFormIdAsync(int manifestFormId, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.Where(item => item.ManifestFormId.HasValue && item.ManifestFormId == manifestFormId)
                                               .Select(item => item.Id)
                                               .ToListAsync(cancellationToken);
    }

    public async Task<List<int>> GetWaybillIdByDomesticWaybillPeriodIdAndLessThanStartNumberAsync(int domesticWaybillPeriodId, long startNumber, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.Where(item => item.DomesticWaybillPeriodId == domesticWaybillPeriodId && item.No < startNumber)
                                               .Select(item => item.Id)
                                               .ToListAsync(cancellationToken);
    }

    public async Task<List<int>> GetWaybillIdByDomesticWaybillPeriodIdAndGreatherThanEndNumberAsync(int domesticWaybillPeriodId, long endNumber, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.Where(item => item.DomesticWaybillPeriodId == domesticWaybillPeriodId && item.No > endNumber)
                                               .Select(item => item.Id)
                                               .ToListAsync(cancellationToken);
    }

    public async Task<List<int>> GetWaybillIdByDomesticWaybillPeriodIdAsync(int domesticWaybillPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.Where(item => item.DomesticWaybillPeriodId == domesticWaybillPeriodId)
                                               .Select(item => item.Id)
                                               .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsManifestStatusConsistentByManifestFormIdStateAsync(int manifestFormId, short state, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.AnyAsync(item => item.ManifestFormId.HasValue && item.ManifestFormId == manifestFormId && item.State == state);
    }

    public async Task<bool> AnyIssunedDomesticWaybillByDomesticPeriodIdStartNumberEndNumberAsync(int domesticPeriod, long startNumber, long endNumber, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.AnyAsync(item => item.DomesticWaybillPeriodId == domesticPeriod && (item.No < startNumber || item.No > endNumber) && item.State != (short)WaybillState.Ready);
    }

    public async Task<bool> AnyIssunedDomesticWaybillByDomesticPeriodIdAsync(int domesticPeriod, CancellationToken cancellationToken)
    {
        return await dbContext.DomesticWaybills.AnyAsync(item => item.DomesticWaybillPeriodId == domesticPeriod && item.State != (short)WaybillState.Ready);
    }

    public async Task ChangeStateAsync(List<int> waybillIds, short state, CancellationToken cancellationToken)
    {
        foreach (var waybillId in waybillIds)
        {
            var waybill = await dbContext.DomesticWaybills.SingleOrDefaultAsync(item => item.Id == waybillId);
            if (waybill == null)
                return;

            switch (state)
            {
                case (short)WaybillState.Issued:
                    waybill.DateIssued = Tools.GetTodayInPersianDate();
                    waybill.TimeIssued = Tools.GetTime();
                    break;
                case (short)WaybillState.Collectiong:
                    waybill.DateCollectiong = Tools.GetTodayInPersianDate();
                    waybill.TimeCollectiong = Tools.GetTime();
                    break;
                case (short)WaybillState.ReceivedAtSenderCompany:
                    waybill.DateReceivedAtSenderCompany = Tools.GetTodayInPersianDate();
                    waybill.TimeReceivedAtSenderCompany = Tools.GetTime();
                    break;
                case (short)WaybillState.Manifested:
                    waybill.DateManifested = Tools.GetTodayInPersianDate();
                    waybill.TimeManifested = Tools.GetTime();
                    break;
                case (short)WaybillState.AirlineDelivery:
                    waybill.DateAirlineDelivery = Tools.GetTodayInPersianDate();
                    waybill.TimeAirlineDelivery = Tools.GetTime();
                    break;
                case (short)WaybillState.ReceivedAtReceiverCompany:
                    waybill.DateReceivedAtReceiverCompany = Tools.GetTodayInPersianDate();
                    waybill.TimeReceivedAtReceiverCompany = Tools.GetTime();
                    break;
                case (short)WaybillState.Distribution:
                    waybill.DateDistribution = Tools.GetTodayInPersianDate();
                    waybill.TimeDistribution = Tools.GetTime();
                    break;
                case (short)WaybillState.Delivery:
                    waybill.DateDelivery = Tools.GetTodayInPersianDate();
                    waybill.TimeDelivery = Tools.GetTime();
                    break;
            }

            waybill.State = state;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task BackFromStateAsync(List<int> waybillIds, short state, CancellationToken cancellationToken)
    {
        foreach (var waybillId in waybillIds)
        {
            var waybill = await dbContext.DomesticWaybills.SingleOrDefaultAsync(item => item.Id == waybillId);
            if (waybill == null)
                return;

            switch (state)
            {
                case (short)WaybillState.Issued:
                    waybill.DateCollectiong = null;
                    waybill.TimeCollectiong = null;
                    waybill.BikeDeliveryInSenderCompanyId = null;
                    waybill.BikeDeliveryInSenderCompanyAgent = null;
                    if (waybill.State == (short)WaybillState.ReceivedAtSenderCompany)
                    {
                        waybill.DateReceivedAtSenderCompany = null;
                        waybill.TimeReceivedAtSenderCompany = null;
                    }
                    break;
                case (short)WaybillState.Collectiong:
                    waybill.DateReceivedAtSenderCompany = null;
                    waybill.TimeReceivedAtSenderCompany = null;
                    break;
                case (short)WaybillState.ReceivedAtSenderCompany:
                    waybill.ManifestFormId = null;
                    waybill.DateManifested = null;
                    waybill.TimeManifested = null;
                    break;
                case (short)WaybillState.Manifested:
                    waybill.DateAirlineDelivery = null;
                    waybill.TimeAirlineDelivery = null;
                    break;
                case (short)WaybillState.AirlineDelivery:
                    waybill.DateReceivedAtReceiverCompany = null;
                    waybill.TimeReceivedAtReceiverCompany = null;
                    break;
                case (short)WaybillState.ReceivedAtReceiverCompany:
                    waybill.DateDistribution = null;
                    waybill.TimeDistribution = null;
                    waybill.BikeDeliveryInReceiverCompanyId = null;
                    waybill.BikeDeliveryInReceiverCompanyAgent = null;
                    if (waybill.State == (short)WaybillState.Delivery)
                    {
                        waybill.DateDelivery = null;
                        waybill.TimeDelivery = null;
                        waybill.CompanyReceiverDateFinancial = null;
                        waybill.CompanyReceiverCashPayment = null;
                        waybill.CompanyReceiverBankPayment = null;
                        waybill.CompanyReceiverCashOnDelivery = null;
                        waybill.CompanyReceiverBankId = null;
                        waybill.CompanyReceiverBankPaymentNo = null;
                        waybill.CompanyReceiverCreditPayment = null;
                        waybill.CompanyReceiverResponsibleCustomerId = null;
                        waybill.EntranceDeliveryPerson = null;
                        waybill.EntranceTransfereePersonName = null;
                        waybill.EntranceTransfereePersonNationalCode = null;
                    }
                    break;
                case (short)WaybillState.Distribution:
                    waybill.DateDelivery = null;
                    waybill.TimeDelivery = null;
                    waybill.CompanyReceiverDateFinancial = null;
                    waybill.CompanyReceiverCashPayment = null;
                    waybill.CompanyReceiverBankPayment = null;
                    waybill.CompanyReceiverCashOnDelivery = null;
                    waybill.CompanyReceiverBankId = null;
                    waybill.CompanyReceiverBankPaymentNo = null;
                    waybill.CompanyReceiverCreditPayment = null;
                    waybill.CompanyReceiverResponsibleCustomerId = null;
                    waybill.EntranceDeliveryPerson = null;
                    waybill.EntranceTransfereePersonName = null;
                    waybill.EntranceTransfereePersonNationalCode = null;
                    break;
            }

            waybill.State = state;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
