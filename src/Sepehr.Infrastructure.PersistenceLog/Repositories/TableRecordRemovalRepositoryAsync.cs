using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.LogEntities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class TableRecordRemovalRepositoryAsync : GenericRepositoryLogAsync<TableRecordRemovalInfo>, ITableRecordRemovalRepositoryAsync
    {
        private readonly DbSet<TableRecordRemovalInfo> _TableRecordRemovals;

        public TableRecordRemovalRepositoryAsync(ApplicationLogDbContext dbContext) : base(dbContext)
        {
            _TableRecordRemovals = dbContext.Set<TableRecordRemovalInfo>();
        }

    }
}