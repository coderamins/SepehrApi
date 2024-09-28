using AutoMapper;
using Confluent.Kafka;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.DTOs.CustomerWarehouse;
using Sepehr.Application.Features.Customers.Queries.GetAllCustomers;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;
using Sepehr.Infrastructure.Persistence.Context;
using System.Data;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class CustomerRepositoryAsync : GenericRepositoryAsync<Customer>, ICustomerRepositoryAsync
    {
        private readonly DapperContext _dapContext;
        private readonly DbSet<Customer> _customers;
        private readonly DbSet<CustomerAssignedLabel> _customerAsignedLabel;
        private readonly DbSet<CustomerWarehouse> _customerWarehouses;
        private readonly DbSet<OrderDetail> _orderDetail;
        private readonly DbSet<CustomerLabel> _customerLabel;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerRepositoryAsync(ApplicationDbContext dbContext, IMapper mapper, DapperContext dapContext) : base(dbContext)
        {
            _customers = dbContext.Set<Customer>();
            _customerWarehouses = dbContext.Set<CustomerWarehouse>();
            _customerAsignedLabel = dbContext.Set<CustomerAssignedLabel>();
            _orderDetail = dbContext.Set<OrderDetail>();
            _customerLabel = dbContext.Set<CustomerLabel>();
            _dbContext = dbContext;
            _mapper = mapper;
            _dapContext = dapContext;
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

        public async Task<IQueryable<Customer>> GetAllCustomers(GetAllCustomersParameter filter)
        {
            filter.Keyword = filter.Keyword.Replace("ﮎ", "ک")
                  .Replace("ﮏ", "ک")
                  .Replace("ﮐ", "ک")
                  .Replace("ﮑ", "ک")
                  .Replace("ك", "ک")
                  .Replace("ي", "ی")
                  .Replace("ئ", "ی")
                  .Replace("ى", "ی")
                  .Replace(" ", " ")
                  .Replace("‌", " ")
                  .Replace("ٔ", "")
                  .Replace("ھ", "ه")
                  .Replace("دِ", "د")
                  .Replace("بِ", "ب")
                  .Replace("زِ", "ز")
                  .Replace("شِ", "ش")
                  .Replace("سِ", "س");

            List<Guid> _label_purchased_customers = new List<Guid>();
            if (filter.ReportType == CustomerReportType.ReportByPurchaseHistory ||
                filter.ReportType == CustomerReportType.BothOfThem)
            {
                var _labelInfo = await _customerLabel.FirstAsync(l => l.Id == filter.CustomerLabelId);

                _label_purchased_customers = await
                    _orderDetail.Where(o =>
                    (_labelInfo.BrandId != null && o.ProductBrand.BrandId == _labelInfo.BrandId) ||
                    (_labelInfo.ProductBrandId != null && o.ProductBrandId == _labelInfo.ProductBrandId) ||
                    (_labelInfo.ProductTypeId != null && o.Product.ProductTypeId == _labelInfo.ProductTypeId) ||
                    (_labelInfo.ProductId != null && o.Product.Id == _labelInfo.ProductId))
                    .Select(d => d.Order.CustomerId).ToListAsync();
            }

            var queryResult = new List<Customer>();

            var customers = _customers
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
                        ((c.Phonebook != null && c.Phonebook.Any(p => p.PhoneNumber.Contains(filter.PhoneNumber)) || filter.CustomerCode == null)) &&
                        ((_label_purchased_customers.Contains(c.Id) && new CustomerReportType[] { CustomerReportType.ReportByPurchaseHistory, CustomerReportType.BothOfThem }.Contains(filter.ReportType)) ||
                        (filter.CustomerLabelId != null &&
                            new CustomerReportType[] { CustomerReportType.ByLabelId, CustomerReportType.BothOfThem }.Contains(filter.ReportType) &&
                            c.CustomerLabels.Select(l => l.CustomerLabelId).Contains((int)filter.CustomerLabelId) || filter.CustomerLabelId == null))
                        )
                .OrderByDescending(p => p.Created).AsQueryable();

            if (string.IsNullOrEmpty(filter.Keyword))
                queryResult.AddRange(customers);

            var query = customers;
            foreach (var item in filter.Keyword.Split(' '))
            {
                query = query.Where(c =>
                        ((c.OfficialName ?? "").Contains(item) ||
                        c.NationalCode.Contains(item) ||
                        c.CustomerCode.ToString().Contains(item) ||
                        c.FirstName.Contains(item) ||
                        c.LastName.Contains(item) ||
                        (c.Phonebook != null && c.Phonebook.Any(p => p.PhoneNumber.Contains(item)))));

                queryResult.AddRange(query);
            }


            foreach (var item in filter.Keyword.Split(' '))
            {
                queryResult.OrderByDescending(x => string.Concat(x.FirstName, ' ', x.LastName).Contains(item) ? 1 : 0)
                    .ThenByDescending(x => x.NationalCode.Contains(item) ? 1 : 0)
                    .ThenByDescending(x => x.CustomerCode.ToString().Contains(item) ? 1 : 0)
                    .ToList();
            }

            return queryResult.DistinctBy(x => x.Id).AsQueryable();
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
                .Include(c => c.CustomerLabels)
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
                .Include(c => c.CustomerLabels).ThenInclude(c => c.CustomerLabel)
                .Include(c => c.CustomerWarehouses).ThenInclude(w => w.Warehouse).ThenInclude(w => w.WarehouseType)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var cust = await _customers.FirstAsync(c => c.Id == customer.Id);

            var cust_phones = _dbContext.Phonebook
                .Where(p => p.CustomerId == customer.Id);
            if (cust_phones != null)
                _dbContext.Phonebook.RemoveRange(cust_phones);

            _customers.Entry(cust).State = EntityState.Modified;
            _customers.Entry(cust).CurrentValues.SetValues(customer);

            _customers.Update(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<List<CustomerBalanceViewModel>> GetCustomerBalance(GetCustomersBalanceParameter filter)
        {

            List<CustomerBalanceViewModel> customerBalances = new List<CustomerBalanceViewModel>();
            // بدهکاری مشتری
            //-----لیست سفارشات فروش به مشتری ------
            var cust_orders = await _dbContext.Set<Order>()
                .Where(o =>
                    o.CustomerId == filter.CustomerId &&
                    (o.Created.Date >= filter.BalanceFromDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.BalanceFromDate)) &&
                    (o.Created.Date <= filter.BalanceToDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.BalanceToDate))
                ).ToListAsync();

            customerBalances.Add(new CustomerBalanceViewModel
            {
                BalanceDate = "",
                Amount = cust_orders.Sum(o => o.Details.Sum(d => d.AlternativeProductBrand != null ? d.AlternativeProductAmount : d.ProximateAmount)),

            });
            //----لیست سفارشاتی که خروج داشته اند-----
            var cust_exited_cargo = await _dbContext.Set<Order>()
                                .Include(x => x.LadingPermits.Where(x => x.LadingExitPermit != null))
                                    .ThenInclude(x => x.LadingExitPermit)
                                .Include(x => x.LadingPermits)
                                    .ThenInclude(x => x.CargoAnnounce)
                                .Where(o => o.LadingPermits.Count() > 0 &&
                                o.CustomerId == filter.CustomerId &&
                                (o.Created.Date >= filter.BalanceFromDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.BalanceFromDate)) &&
                                (o.Created.Date <= filter.BalanceToDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.BalanceToDate)))
                                .ToListAsync();

            //----لیست مجوز های خروج-----
            var exitPermits = await _dbContext.Set<LadingExitPermit>()
                                .Include(x => x.LadingExitPermitDetails
                                        .Where(x => x.CargoAnnounceDetail.CargoAnnounce.Order.CustomerId == filter.CustomerId))
                                .ThenInclude(x => x.CargoAnnounceDetail).ThenInclude(x => x.CargoAnnounce).ThenInclude(x => x.Order)
                                .Where(o =>
                                (o.Created.Date >= filter.BalanceFromDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.BalanceFromDate)) &&
                                (o.Created.Date <= filter.BalanceToDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.BalanceToDate)))
                                .ToListAsync();

            foreach (var item in exitPermits)
            {
                customerBalances.Add(new CustomerBalanceViewModel
                {
                    BalanceDate = item.Created.ToShamsiDate(),
                    Amount = cust_orders.Sum(o => o.Details.Sum(d => d.AlternativeProductBrand != null ? d.AlternativeProductAmount : d.ProximateAmount)),

                });
            }
            // بستانکاری مشتری
            //-----لیست سفارشات خرید از مشتری ------
            var purchase_orders =
                await _dbContext.Set<PurchaseOrder>()
                .Where(o =>
                       o.CustomerId == filter.CustomerId &&
                       (o.Created.Date >= filter.BalanceFromDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.BalanceFromDate)) &&
                       (o.Created.Date <= filter.BalanceToDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.BalanceToDate))
                ).ToListAsync();

            //-----لیست سفارشاتی که تخلیه بار شده اند------
            var cust_unloaded_orders = await _dbContext.Set<PurchaseOrder>()
                    .Include(x => x.TransferRemittances.Where(x => x.EntrancePermit != null && x.EntrancePermit.UnloadingPermit != null))
                        .ThenInclude(x => x.EntrancePermit)
                        .ThenInclude(x => x.UnloadingPermit)
                    .Where(x => x.CustomerId == filter.CustomerId &&
                           (x.Created.Date >= filter.BalanceFromDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.BalanceFromDate)) &&
                           (x.Created.Date <= filter.BalanceToDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.BalanceToDate)))
                    .ToListAsync();


            #region مانده بستانکاری مشتری
            //-----لیست پرداخت های بازرگانی به مشتری ------
            var receive_payments = await _dbContext.Set<ReceivePay>()
                .Where(r => r.PayToCustomerId == filter.CustomerId && r.ReceivePayStatusId == (int)EReceivePayStatus.AccApproved).ToListAsync();

            var cust_pay_requests = await _dbContext.Set<PaymentRequest>()
                .Where(x => x.CustomerId == filter.CustomerId && x.PaymentRequestStatusId == (int)EPaymentRequestStatus.Payed).ToListAsync();

            #endregion

            decimal cust_creditor = (purchase_orders.Sum(o => o.TotalAmount) +
                                    receive_payments.Sum(x => x.Amount));
            decimal dept =
                (cust_orders.Sum(c => c.TotalAmount) +
                cust_pay_requests.Sum(x => x.Amount));

            //return new CustomerBalanceViewModel
            //{

            //};
            return null;
        }

        public async Task<bool> AssignCustomerLabels(ICollection<CustomerAssignedLabel> customerLabels)
        {
            var customerAsignedLables =
                await _customerAsignedLabel
                .Where(x => x.CustomerId == customerLabels
                .First().CustomerId)
                .ToListAsync();

            if (customerAsignedLables != null)
                _customerAsignedLabel.RemoveRange(customerAsignedLables);

            await _customerAsignedLabel.AddRangeAsync(customerLabels);

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CustomerBillingViewModel> GetCustomerBillingReport(GetCustomerBillingParameter validFilter)
        {
            try
            {
                List<CustomerBillingDetailViewModel> customerMovedInAdvanceBillingReport = new List<CustomerBillingDetailViewModel>();
                var proc_params = new DynamicParameters();

                if (validFilter.DateFilter == 1)
                {
                    proc_params.Add("@Date", validFilter.FromDate.ToDateTime("00:00"));
                    proc_params.Add("@CustomerId", validFilter.CustomerId);
                    proc_params.Add("@ReportType", validFilter.BillingReportType);

                    using (var connection = _dapContext.CreateConnection())
                    {
                        var dcustomerMovedInAdvanceBillingReport = connection
                            .Query("SP_CustomerMovedInAdvanceBilling", proc_params, commandType: CommandType.StoredProcedure).ToList();

                        foreach (var item in customerMovedInAdvanceBillingReport)
                        {
                            item.WeightingDate_Shamsi = "";
                            item.Created_Shamsi = item.Created.ToShamsiDate();
                        }
                    }
                }

                proc_params = new DynamicParameters();

                proc_params.Add("@DateFilter", validFilter.DateFilter);
                proc_params.Add("@FromDate", validFilter.DateFilter == -1 ? DateTime.Now : validFilter.FromDate.ToDateTime("00:00"));
                proc_params.Add("@ToDate", validFilter.DateFilter == -1 ? DateTime.Now : validFilter.ToDate.ToDateTime("00:00"));
                proc_params.Add("@CustomerId", validFilter.CustomerId);
                proc_params.Add("@ReportType", validFilter.BillingReportType);

                DateTime defaultDate = new DateTime(1900, 1, 1);
                using (var connection = _dapContext.CreateConnection())
                {
                    var customebillingReport = connection
                        .Query<CustomerBillingDetailViewModel>("SP_CustomerBilling", proc_params, commandType: CommandType.StoredProcedure).ToList();

                    foreach (var item in customebillingReport)
                    {
                        item.WeightingDate_Shamsi = !item.WeightingDate.Equals(defaultDate) ? item.WeightingDate.ToShamsiDate() : "";
                        item.Created_Shamsi = item.Created.ToShamsiDate();
                    }

                    if (customerMovedInAdvanceBillingReport.Count() > 0 && customebillingReport.Count() > 0)
                        customebillingReport[0].DueRemainingAmount += customerMovedInAdvanceBillingReport[0].RemainingAmount;

                    var result = customebillingReport.Union(customerMovedInAdvanceBillingReport).OrderBy(x => x.Created).ToList();
                    for (int i = 0; i <= result.Count() - 1; i++)
                    {
                        var prevBill = i == 0 ? null : result[i - 1];
                        var currentBill = result[i];

                        //-----مانده= بستانکاری ردیف فعلی + بدهکاری ردیف قبلی - بدهکاری ردیف فعلی
                        result[i].RemainingAmount = currentBill.DebitAmount - currentBill.CreditAmount + (prevBill == null ? 0 : prevBill.DebitAmount);

                        //-----مانده موعد شده = بستانکاری ردیف فعلی - مانده موعد شده ردیف قبلی
                        result[i].DueRemainingAmount += (prevBill == null ? 0 : prevBill.DueRemainingAmount) - currentBill.CreditAmount;

                        result[i].Recognizing = result[i].DebitAmount > result[i].CreditAmount ? "بد" :
                                               result[i].DebitAmount < result[i].CreditAmount ? "بس" : "-";

                    }

                    return new CustomerBillingViewModel
                    {
                        CustomerId = validFilter.CustomerId,
                        RemainingAmount = result.Count() <= 0 ? 0 : result.Last().RemainingAmount,
                        Recognize = result.Count() <= 0 ? "" : result.Last().RemainingAmount > 0 ? "بد" : "بس",
                        TotalDueRemainingAmount = result.Sum(x => x.DueRemainingAmount),
                        Details = result
                    };
                }
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}