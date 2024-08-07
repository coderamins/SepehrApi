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
                .Include(x => x.CustomerLabelType)
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
                .Include(x => x.CustomerLabelType)
                .Include(x => x.Product)
                .Include(x => x.ProductType)
                .Include(x => x.Brand)
                .Include(x => x.ProductBrand).ThenInclude(x => x.Brand)
                .Include(x => x.ProductBrand).ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(
                    c =>
                        ((
                        (c.LabelName != null && c.LabelName == filter.LabelName) ||
                        (filter.ProductBrandId != null && c.ProductBrandId == filter.ProductBrandId) ||
                        (filter.BrandId != null && c.BrandId == filter.BrandId) ||
                        (filter.ProductTypeId != null && c.ProductTypeId == filter.ProductTypeId) ||
                        (filter.ProductId != null && c.ProductId == filter.ProductId))
                         ) && c.CustomerLabelTypeId == filter.CustomerLabelTypeId);
        }

        public async Task<CustomerLabel?> GetCustomerLabelInfo(int Id)
        {
            return await _customerLabels
                .Include(x => x.CustomerLabelType)
                .Include(x => x.Product)
                .Include(x => x.ProductType)
                .Include(x => x.Brand)
                .Include(x => x.ProductBrand).ThenInclude(x => x.Brand)
                .Include(x => x.ProductBrand).ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(c =>c.Id==Id);
        }
    }
}