using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.CustomerOfficialCompanys.Queries.GetAllCustomerOfficialCompanys;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class CustomerOfficialCompanyRepositoryAsync : GenericRepositoryAsync<CustomerOfficialCompany>, ICustomerOfficialCompanyRepositoryAsync
    {
        private readonly DbSet<CustomerOfficialCompany> _customerOfficialCompanys;

        public CustomerOfficialCompanyRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _customerOfficialCompanys = dbContext.Set<CustomerOfficialCompany>();
        }

        public async Task<List<CustomerOfficialCompany>> GetAllCustomerOfficialCompanies(GetAllCustomerOfficialCompanysParameter filter)
        {
            return await _customerOfficialCompanys
                .Include(c=>c.Customer)
                .Where(c=>
                    (c.CustomerId==filter.CustomerId || filter.CustomerId==null) &&
                    (c.IsActive==filter.IsActive || filter.IsActive==null))
                .OrderByDescending(p => p.Created).ToListAsync();
        }

        public async Task<CustomerOfficialCompany?> GetCustomerOfficialCompany(int companyId)
        {
            return await _customerOfficialCompanys
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(p => p.Id== companyId);
        }
    }
}