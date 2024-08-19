using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.EntrancePermits.Queries.GetAllEntrancePermits;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class EntrancePermitRepositoryAsync : GenericRepositoryAsync<EntrancePermit>, IEntrancePermitRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TransferRemittance> _transferRemittances;
        private readonly DbSet<ProductInventory> _prodInventory;
        private readonly IMapper _mapper;
        public EntrancePermitRepositoryAsync(
            ApplicationDbContext dbContext,
            IMapper mapper
            ) : base(dbContext)
        {
            _dbContext = dbContext;
            _transferRemittances = dbContext.Set<TransferRemittance>();
            _mapper = mapper;
            _prodInventory= dbContext.Set<ProductInventory>();
        }

        public async Task<EntrancePermit> CreateEntrancePermit(EntrancePermit entrancePermit)
        {
            var transRemit = await _dbContext.TransferRemittances
                   .AsNoTracking()
                   .FirstOrDefaultAsync(o => o.Id == entrancePermit.TransferRemittanceId);

            if (transRemit == null)
                throw new ApiException("حواله انتقال یافت نشد !");

            if (transRemit.TransferRemittanceStatusId == 2)
                throw new ApiException("مجوز ورود حواله قبلا ثبت شده است !");
            if (transRemit == null)
                throw new ApiException("حواله یافت نشد !");

            //------موجودی در راه انبار مقصد کم می شود-----
            foreach(var item in transRemit.Details)
            {
                var _inv=await _prodInventory
                    .FirstOrDefaultAsync(x=>x.ProductBrandId==item.ProductBrandId && x.WarehouseId== transRemit.DestinationWarehouseId);
                if (_inv == null)
                    throw new ApiException("موجودی محصول یافت نشد !");

                var entr = _prodInventory.Entry(_inv);
                _inv.OnTransitInventory-=item.TransferAmount;

                entr.State = EntityState.Modified;
                entr.CurrentValues.SetValues(_inv);
            }

            var trnsRemitEntry = _transferRemittances.Entry(transRemit);
            transRemit.TransferRemittanceStatusId = 2;

            trnsRemitEntry.State = EntityState.Modified;
            trnsRemitEntry.CurrentValues.SetValues(transRemit);            

            var newEntrancePermit = _mapper.Map<EntrancePermit>(entrancePermit);
            await _dbContext.AddAsync(newEntrancePermit);

            await _dbContext.SaveChangesAsync();
            return newEntrancePermit;
        }

        public async Task DeleteEntrancePermit(Guid id)
        {
            var entrancePermit = await _dbContext.EntrancePermits.FirstOrDefaultAsync(x=>x.Id==id);

            if (entrancePermit == null)
                throw new ApiException("مجوز ورود یافت نشد !");

            var transferRemitt = await _dbContext.TransferRemittances
                .FirstAsync(x => x.Id == entrancePermit.TransferRemittanceId);

            transferRemitt.TransferRemittanceStatusId =(int)ETransferRemittanceStatus.InProgress;
            _transferRemittances.Update(transferRemitt);

            _dbContext.EntrancePermits.Remove(entrancePermit);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<EntrancePermit>> GetAllEntrancePermitsAsync(GetAllEntrancePermitsParameter validFilter)
        {
            return await
                _dbContext.EntrancePermits
                .Include(x=>x.ApplicationUser)
                .Include(x => x.TransferRemittance).ThenInclude(x => x.TransferRemittanceStatus)
                .Include(x => x.TransferRemittance).ThenInclude(x => x.TransferRemittanceType)
                .Include(x => x.TransferRemittance).ThenInclude(x => x.OriginWarehouse)
                .Include(x => x.TransferRemittance).ThenInclude(x => x.DestinationWarehouse)
                .ToListAsync();
        }

        public async Task<EntrancePermit?> GetEntrancePermitById(Guid id)
        {
            return await
                _dbContext.EntrancePermits
                .Include(x => x.UnloadingPermits).ThenInclude(x=>x.UnloadingPermitDetails)
                .Include(x => x.ApplicationUser)
                .Include(x => x.TransferRemittance).ThenInclude(x => x.TransferRemittanceStatus)
                .Include(x => x.TransferRemittance).ThenInclude(x => x.TransferRemittanceType)
                .Include(x => x.TransferRemittance).ThenInclude(x => x.OriginWarehouse)
                .Include(x => x.TransferRemittance).ThenInclude(x => x.DestinationWarehouse)
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<EntrancePermit> UpdateEntrancePermit(EntrancePermit entrancePermit)
        {
            throw new NotImplementedException();
        }
    }
}
