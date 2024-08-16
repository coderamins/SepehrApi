using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Sepehr.Application.Exceptions;
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
        private readonly ApplicationDbContext _dbContext;

        public CustomerLabelRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _customerLabels = dbContext.Set<CustomerLabel>();
            _dbContext = dbContext;
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
                .Where(x => 
                        (x.CustomerLabelTypeId == filter.CustomerLabelTypeId || filter.CustomerLabelTypeId==null))
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
                .FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<CustomerLabel> UpdateCustomerLabelAsync(CustomerLabel customerLabel)
        {
            var cl=await _customerLabels.FirstAsync(c=>c.Id==customerLabel.Id);
            if (cl == null)
                throw new ApiException("برچسب مشتری یافت نشد !");

            _dbContext.Entry(cl).State = EntityState.Modified;
            _dbContext.Entry(cl).CurrentValues.SetValues(customerLabel);
            await _dbContext.SaveChangesAsync();

            return customerLabel;
        }
    }
}