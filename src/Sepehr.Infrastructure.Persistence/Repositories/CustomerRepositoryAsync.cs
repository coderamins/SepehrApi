using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.DTOs.CustomerWarehouse;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class CustomerRepositoryAsync : GenericRepositoryAsync<Customer>, ICustomerRepositoryAsync
    {
        private readonly DbSet<Customer> _customers;
        private readonly DbSet<CustomerWarehouse> _customerWarehouses;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerRepositoryAsync(ApplicationDbContext dbContext,IMapper mapper) : base(dbContext)
        {
            _customers = dbContext.Set<Customer>();
            _customerWarehouses = dbContext.Set<CustomerWarehouse>();
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> AllocateCustomerWarehouses(Guid CustomerId, List<int> warehouses)
        {
            var customer=await _customers.FirstOrDefaultAsync(c => c.Id == CustomerId);
            _dbContext.CustomerWarehouses.RemoveRange(_dbContext.CustomerWarehouses
                .Where(s => s.CustomerId == CustomerId && !warehouses.Contains(s.Id)));

            
            foreach(var w in warehouses)
            {
                var customerWarehouse=_mapper.Map<CustomerWarehouse>(new CustomerWarehouseDto
                {
                    CustomerId= CustomerId,
                    WarehouseId=w
                });

                await _dbContext.CustomerWarehouses.AddAsync(customerWarehouse);
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customers
                .Include(c=>c.CustomerValidity)
                .Include(c=>c.CustomerOfficialCompanies)
                .Include(c => c.ReceivePaymentSourceFrom)
                .Include(c => c.ReceivePaymentSourceTo)
                .Include(c => c.Orders)
                .Include(c => c.CustomerWarehouses).ThenInclude(w=>w.Warehouse).ThenInclude(w=>w.WarehouseType)
                .OrderByDescending(p => p.Created).ToListAsync();
        }

        public async Task<Customer?> GetCustomerInfo(string nationalId)
        {
            return await _customers
                .Include(c => c.CustomerOfficialCompanies)
                .Include(c => c.CustomerValidity)
                .Include(c=>c.ReceivePaymentSourceFrom)
                .Include(c=>c.ReceivePaymentSourceTo)
                .Include(c=>c.Orders)
                .Include(c => c.CustomerWarehouses).ThenInclude(w => w.Warehouse).ThenInclude(w => w.WarehouseType)
                .FirstOrDefaultAsync(p => p.NationalId==nationalId);
        }

        public async Task<Customer?> GetCustomerInfo(Guid Id)
        {
            return await _customers
                .Include(c => c.CustomerOfficialCompanies)
                .Include(c => c.CustomerValidity)
                .Include(c => c.ReceivePaymentSourceFrom)
                .Include(c => c.ReceivePaymentSourceTo)
                .Include(c=>c.Orders)
                .Include(c => c.CustomerWarehouses).ThenInclude(w => w.Warehouse).ThenInclude(w => w.WarehouseType)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }
    }
}