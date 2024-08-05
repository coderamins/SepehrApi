using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.DTOs.CustomerWarehouse;
using Sepehr.Application.Features.Customers.Queries.GetAllCustomers;
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

        public CustomerRepositoryAsync(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _customers = dbContext.Set<Customer>();
            _customerWarehouses = dbContext.Set<CustomerWarehouse>();
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> AllocateCustomerWarehouses(Guid CustomerId, List<int> warehouses)
        {
            var customer = await _customers.FirstOrDefaultAsync(c => c.Id == CustomerId);
            _dbContext.CustomerWarehouses.RemoveRange(_dbContext.CustomerWarehouses
                .Where(s => s.CustomerId == CustomerId && !warehouses.Contains(s.Id)));


            foreach (var w in warehouses)
            {
                var customerWarehouse = _mapper.Map<CustomerWarehouse>(new CustomerWarehouseDto
                {
                    CustomerId = CustomerId,
                    WarehouseId = w
                });

                await _dbContext.CustomerWarehouses.AddAsync(customerWarehouse);
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Customer>> GetAllCustomers(GetAllCustomersParameter filter)
        {
            return await _customers
                .Include(c => c.CustomerValidity)
                .Include(c => c.ApplicationUser)
                .Include(c => c.Phonebook).ThenInclude(p => p.PhoneNumberType)
                .Include(c => c.CustomerOfficialCompanies)
                .Include(c => c.ReceivePaymentSourceFrom)
                .Include(c => c.ReceivePaymentSourceTo)
                .Include(c => c.Orders)
                .Include(c => c.Phonebook)
                .Include(c => c.CustomerWarehouses).ThenInclude(w => w.Warehouse).ThenInclude(w => w.WarehouseType)
                .Where(c =>
                        (c.NationalCode == filter.NationalCode || string.IsNullOrEmpty(filter.NationalCode)) &&
                        (c.CustomerCode == filter.CustomerCode || filter.CustomerCode == null) &&
                        (string.Concat(c.FirstName, " ", c.LastName).Contains(filter.CustomerName) || string.IsNullOrEmpty(filter.CustomerName)) &&
                        ((c.Phonebook != null && c.Phonebook.Any(p => p.PhoneNumber.Contains(filter.PhoneNumber)) || filter.CustomerCode == null)))
                .OrderByDescending(p => p.Created).ToListAsync();
        }

        public async Task<Customer?> GetCustomerInfo(string nationalId)
        {
            return await _customers
                .Include(c => c.Phonebook).ThenInclude(p => p.PhoneNumberType)
                .Include(c => c.CustomerOfficialCompanies)
                .Include(c => c.CustomerValidity)
                .Include(c => c.ApplicationUser)
                .Include(c => c.ReceivePaymentSourceFrom)
                .Include(c => c.ReceivePaymentSourceTo)
                .Include(c => c.Orders)
                .Include(c => c.CustomerWarehouses).ThenInclude(w => w.Warehouse).ThenInclude(w => w.WarehouseType)
                .FirstOrDefaultAsync(p => p.NationalId == nationalId);
        }

        public async Task<Customer?> GetCustomerInfo(Guid Id)
        {
            return await _customers
                .Include(c => c.Phonebook).ThenInclude(p => p.PhoneNumberType)
                .Include(c => c.CustomerOfficialCompanies)
                .Include(c => c.CustomerValidity)
                .Include(c => c.ApplicationUser)
                .Include(c => c.ReceivePaymentSourceFrom)
                .Include(c => c.ReceivePaymentSourceTo)
                .Include(c => c.Orders)
                .Include(c => c.CustomerWarehouses).ThenInclude(w => w.Warehouse).ThenInclude(w => w.WarehouseType)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Customer> UpdateCustomer(Customer customer, Customer old_customer)
        {
            var cust_phones = _dbContext.Phonebook
                .Where(p => p.CustomerId == customer.Id);
            if (cust_phones != null)
                _dbContext.Phonebook.RemoveRange(cust_phones);

            _customers.Entry(old_customer).CurrentValues.SetValues(customer);

            _customers.Update(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> AssignCustomerLabels(ICollection<CustomerLabel> customerLabels)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AssignCustomerLabels(ICollection<CustomerAssignedLabel> customerLabels)
        {
            throw new NotImplementedException();
        }
    }
}