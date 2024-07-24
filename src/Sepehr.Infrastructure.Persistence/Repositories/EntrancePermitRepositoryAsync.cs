using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.EntrancePermits.Queries.GetAllEntrancePermits;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
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
        private readonly DbSet<PurchaseOrderTransferRemittance> _transferRemittances;
        private readonly IMapper _mapper;
        public EntrancePermitRepositoryAsync(
            ApplicationDbContext dbContext,
            DbSet<PurchaseOrderTransferRemittance> transferRemittances,
            IMapper mapper
            ) : base(dbContext)
        {
            _dbContext = dbContext;
            _transferRemittances = transferRemittances;
            _mapper = mapper;
        }

        public async Task<EntrancePermit> CreateEntrancePermit(EntrancePermit entrancePermit)
        {
            var transRemit = await _dbContext.TransferRemittances
                   .AsNoTracking()
                   .FirstOrDefaultAsync(o => o.Id == entrancePermit.PurchaseOrderTransferRemittanceId);

            if (transRemit == null)
                throw new ApiException("حواله انتقال یافت نشد !");

            if (transRemit.TransferRemittanceStatusId == 2)
                throw new ApiException("مجوز ورود حواله قبلا ثبت شده است !");
            if (transRemit == null)
                throw new ApiException("حواله یافت نشد !");

            transRemit.TransferRemittanceStatusId = 2;
            _transferRemittances.Update(transRemit);

            var newEntrancePermit = _mapper.Map<EntrancePermit>(entrancePermit);
            await _dbContext.AddAsync(newEntrancePermit);

            await _dbContext.SaveChangesAsync();

            return newEntrancePermit;
        }

        public async Task<List<EntrancePermit>> GetAllEntrancePermitsAsync(GetAllEntrancePermitsParameter validFilter)
        {
            throw new NotImplementedException();
        }
    }
}
