using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.CustomerOfficialCompanys.Queries.GetAllCustomerOfficialCompanys;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class CustomerOfficialCompanyRepositoryAsync : GenericRepositoryAsync<CustomerOfficialCompany>,
        ICustomerOfficialCompanyRepositoryAsync
    {
        private readonly DbSet<CustomerOfficialCompany> _customerOfficialCompanys;
        private readonly ApplicationDbContext _dbContext;

        public CustomerOfficialCompanyRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _customerOfficialCompanys = dbContext.Set<CustomerOfficialCompany>();
            _dbContext = dbContext;
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

        public async Task<CustomerOfficialCompany?> GetCustomerOfficialCompanyById(int companyId)
        {
            return await _customerOfficialCompanys
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(p => p.Id== companyId);
        }

        public async Task<CustomerOfficialCompany> UpdateOfficialCompanyAsync(CustomerOfficialCompany customerOfficialCompany)
        {
            var c = await _customerOfficialCompanys.FirstAsync(x => x.Id == customerOfficialCompany.Id);

            _dbContext.Entry(c).State = EntityState.Modified;
            _dbContext.Entry(c).CurrentValues.SetValues(customerOfficialCompany);
            await _dbContext.SaveChangesAsync();

            return customerOfficialCompany;
        }
    }
}