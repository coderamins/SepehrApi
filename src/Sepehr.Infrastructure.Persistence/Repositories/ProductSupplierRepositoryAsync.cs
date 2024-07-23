using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class ProductSupplierRepositoryAsync : GenericRepositoryAsync<ProductSupplier>, IProductSupplierRepositoryAsync
    {
        private readonly DbSet<ProductSupplier> _productSuppliers;

        public ProductSupplierRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _productSuppliers = dbContext.Set<ProductSupplier>();
        }

        public async Task<List<ProductSupplier>> GetAllProductSuppliers()
        {
            return await _productSuppliers
                .Include(s => s.Customer).ThenInclude(c => c.CustomerOfficialCompanies)
                .Include(s => s.Product)
                .OrderByDescending(p => p.Created).ToListAsync();
        }

        public async Task<ProductSupplier?> GetAllProductSupplierById(Guid suppId)
        {
            return await _productSuppliers
                .Include(s => s.Customer).ThenInclude(c => c.CustomerOfficialCompanies)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(p => p.Id== suppId);
        }


    }
}