using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class DriverFareAmountApproveRepositoryAsync : 
    GenericRepositoryAsync<DriverFareAmountApprove>, IDriverFareAmountApproveRepositoryAsync
    {
        private readonly DbSet<DriverFareAmountApprove> _driverFareAmountApproves;
        private readonly ApplicationDbContext _dbContext;

        public DriverFareAmountApproveRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _driverFareAmountApproves = dbContext.Set<DriverFareAmountApprove>();
            _dbContext=dbContext;
        }


    }
}