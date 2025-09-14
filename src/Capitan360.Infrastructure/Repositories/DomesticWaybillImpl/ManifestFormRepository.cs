using Capitan360.Domain.Abstractions;
using Capitan360.Domain.Enums;
using Capitan360.Domain.Interfaces.Repositories.ManifestForm;
using Capitan360.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Capitan360.Infrastructure.Repositories.DomesticWaybillImpl;

public class ManifestFormRepository(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : IManifestFormRepository
{
    public async Task<Domain.Entities.DomesticWaybillEntity.ManifestForm?> GetManifestFormByIdAsync(int manifestFormId, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.DomesticWaybillEntity.ManifestForm> query = dbContext.ManifestForms;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.Counter);
        }

        if (loadDataReceiverCompany)
        {
        }

        return await query.SingleOrDefaultAsync(item => item.Id == manifestFormId, cancellationToken);
    }

    public async Task<Domain.Entities.DomesticWaybillEntity.ManifestForm?> GetManifestFormByNoAsync(long manifestFormNo, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.DomesticWaybillEntity.ManifestForm> query = dbContext.ManifestForms;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.Counter);
        }

        if (loadDataReceiverCompany)
        {
        }

        return await query.SingleOrDefaultAsync(item => item.No == manifestFormNo, cancellationToken);
    }

    public async Task<Domain.Entities.DomesticWaybillEntity.ManifestForm?> GetManifestFormByNoAndCompanySenderIdAsync(long manifestFormNo, int companySenderId, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.DomesticWaybillEntity.ManifestForm> query = dbContext.ManifestForms;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.Counter);
        }

        if (loadDataReceiverCompany)
        {
        }

        return await query.SingleOrDefaultAsync(item => item.No == manifestFormNo && item.CompanySenderId == companySenderId, cancellationToken);
    }

    public async Task<Domain.Entities.DomesticWaybillEntity.ManifestForm?> GetManifestFormByNoAndCompanyReceiverIdAsync(long manifestFormNo, int companyReceiverId, bool tracked, bool loadDataGeneralData, bool loadDataSenderCompany, bool loadDataReceiverCompany, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.DomesticWaybillEntity.ManifestForm> query = dbContext.ManifestForms;

        if (!tracked)
            query = query.AsNoTracking();

        if (loadDataGeneralData)
        {
            query = query.Include(item => item.CompanyReceiver);
            query = query.Include(item => item.CompanySender);
            query = query.Include(item => item.DestinationCity);
            query = query.Include(item => item.SourceCity);
        }

        if (loadDataSenderCompany)
        {
            query = query.Include(item => item.Counter);
        }

        if (loadDataReceiverCompany)
        {
        }

        return await query.SingleOrDefaultAsync(item => item.No == manifestFormNo && item.CompanyReceiverId == companyReceiverId, cancellationToken);
    }

    public async Task<bool> CheckExistManifestFormByIdAsync(int manifestFormId, CancellationToken cancellationToken)
    {
        return await dbContext.ManifestForms.AnyAsync(item => item.Id == manifestFormId, cancellationToken);
    }

    public async Task<bool> CheckExistManifestFormByNoAsync(long manifestFormNo, CancellationToken cancellationToken)
    {
        return await dbContext.ManifestForms.AnyAsync(item => item.No == manifestFormNo, cancellationToken);
    }

    public async Task<bool> CheckExistManifestFormByNoAndCompanySenderIdAsync(long manifestFormNo, int companySenderId, CancellationToken cancellationToken)
    {
        return await dbContext.ManifestForms.AnyAsync(item => item.No == manifestFormNo && item.CompanySenderId == companySenderId, cancellationToken);
    }

    public async Task<bool> CheckExistManifestFormByNoAndCompanyReceiverIdAsync(long manifestFormNo, int companyReceiverId, CancellationToken cancellationToken)
    {
        return await dbContext.ManifestForms.AnyAsync(item => item.No == manifestFormNo && item.CompanyReceiverId == companyReceiverId, cancellationToken);
    }

    public async Task<int> InsertManifestFormAsync(Capitan360.Domain.Entities.DomesticWaybillEntity.ManifestForm manifestForm, CancellationToken cancellationToken)
    {
        dbContext.ManifestForms.Add(manifestForm);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return manifestForm.Id;
    }

    public async Task DeleteManifestFormAsync(int manifestFormId, CancellationToken cancellationToken)
    {
        dbContext.ManifestForms.Remove(await dbContext.ManifestForms.SingleOrDefaultAsync(item => item.Id == manifestFormId, cancellationToken));
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAndLessThanStartNumberAsync(int manifestFormPeriodId, long startNumber, CancellationToken cancellationToken)
    {
        return await dbContext.ManifestForms.Where(item => item.ManifestFormPeriodId == manifestFormPeriodId && item.No < startNumber).Select(item => item.Id).ToListAsync(cancellationToken);
    }

    public async Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAndGreatherThanEndNumberAsync(int manifestFormPeriodId, long endNumber, CancellationToken cancellationToken)
    {
        return await dbContext.ManifestForms.Where(item => item.ManifestFormPeriodId == manifestFormPeriodId && item.No > endNumber).Select(item => item.Id).ToListAsync(cancellationToken);
    }

    public async Task<List<int>> GetManifestFormIdByManifestFormPeriodIdAsync(int manifestFormPeriodId, CancellationToken cancellationToken)
    {
        return await dbContext.ManifestForms.Where(item => item.ManifestFormPeriodId == manifestFormPeriodId).Select(item => item.Id).ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyIssunedManifestFormByDomesticPeriodIdStartNumberEndNumberAsync(int domesticPeriod, long startNumber, long endNumber, CancellationToken cancellationToken)
    {
        return await dbContext.ManifestForms.AnyAsync(item => item.ManifestFormPeriodId == domesticPeriod && (item.No < startNumber || item.No > endNumber) && item.State != (short)WaybillState.Ready);
    }

    public async Task<bool> AnyIssunedManifestFormByDomesticPeriodIdAsync(int domesticPeriod, CancellationToken cancellationToken)
    {
        return await dbContext.ManifestForms.AnyAsync(item => item.ManifestFormPeriodId == domesticPeriod && item.State != (short)WaybillState.Ready);
    }

    public async Task ChangeStateAsync(List<int> waybillIds, short state, CancellationToken cancellationToken)
    {
        foreach (var waybillId in waybillIds)
        {
            var waybill = await dbContext.ManifestForms.SingleOrDefaultAsync(item => item.Id == waybillId);
            waybill.State = state;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
