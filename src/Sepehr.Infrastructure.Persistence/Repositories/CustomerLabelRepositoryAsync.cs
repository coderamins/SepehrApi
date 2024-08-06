using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.CustomerLabels.Command.CreateCustomerLabel;
using Sepehr.Application.Features.Orders.Queries.GetAllOrders;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class CustomerLabelRepositoryAsync : GenericRepositoryAsync<CustomerLabel>, ICustomerLabelRepositoryAsync
    {
        private readonly DbSet<CustomerLabel> _customerLabels;

        public CustomerLabelRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _customerLabels = dbContext.Set<CustomerLabel>();
        }

        public async Task<List<CustomerLabel>> GetAllCustomerLabelsAsync(GetAllCustomerLabelsParameter filter)
        {
            return await _customerLabels
                .Include(x => x.Product)
                .Include(x => x.ProductType)
                .Include(x => x.Brand)
                .Include(x => x.ProductBrand).ThenInclude(x => x.Brand)
                .Include(x => x.ProductBrand).ThenInclude(x => x.Product)
                .ToListAsync();
        }

        public async Task<CustomerLabel?> GetCustomerLabelInfo(CreateCustomerLabelCommand filter)
        {
            return await _customerLabels
                .Include(x=>x.Product)
                .Include(x=>x.ProductType)
                .Include(x=>x.Brand)
                .Include(x=>x.ProductBrand).ThenInclude(x=>x.Brand)
                .Include(x=>x.ProductBrand).ThenInclude(x=>x.Product)
                .FirstOrDefaultAsync(
                    c =>
                        (c.LabelName == filter.LabelName || string.IsNullOrEmpty(filter.LabelName)));
        }
    }
}