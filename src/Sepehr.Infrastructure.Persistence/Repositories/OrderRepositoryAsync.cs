using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.DTOs.Email;
using Sepehr.Application.DTOs.Sms;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllNotSendedOrders;
using Sepehr.Application.Features.Orders.Queries.GetAllOrders;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;
using Sepehr.Infrastructure.Persistence.Context;
using System.Data;
using System.Drawing.Printing;
using System.Linq;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class OrderRepositoryAsync : GenericRepositoryAsync<Order>, IOrderRepositoryAsync
    {
        private readonly DbSet<Order> _orders;
        private readonly DbSet<OrderDetail> _orderDetail;
        private readonly DbSet<OrderService> _orderServices;
        private readonly DbSet<OrderPayment> _orderPayments;
        private readonly DbSet<Warehouse> _warehouses;
        private readonly DbSet<ProductInventory> _productInventory;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly ApplicationDbContext _dbContext;
        private readonly DapperContext _dapContext;

        public OrderRepositoryAsync(
            ApplicationDbContext dbContext
            , DapperContext dapContext
            , IEmailService emailService
            , ISmsService smsService
            , IAuthenticatedUserService authenticatedUser
            ) : base(dbContext)
        {
            _orderPayments = dbContext.Set<OrderPayment>();
            _orderServices = dbContext.Set<OrderService>();
            _orderDetail = dbContext.Set<OrderDetail>();
            _warehouses = dbContext.Set<Warehouse>();
            _orders = dbContext.Set<Order>();
            _emailService = emailService;
            _smsService = smsService;
            _dbContext = dbContext;
            _dapContext = dapContext;
            _productInventory = dbContext.Set<ProductInventory>();
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            //----اگه سفارش فروش فوری باشد از موجودی کسر خواهد شد----
            if (order.OrderTypeId == OrderType.Urgant)
            {
                foreach (var prodBrand in order.Details)
                {
                    var prodInventory = await _productInventory
                        //.Include(i => i.Warehouse).AsNoTracking()
                        .FirstOrDefaultAsync(i => i.ProductBrandId == prodBrand.ProductBrandId &&
                                    i.WarehouseId == prodBrand.WarehouseId);

                    if (prodInventory == null)
                    {
                        foreach (var wh in _warehouses.Where(w => w.WarehouseTypeId != 5).ToList())
                        {
                            await _productInventory.AddAsync(new ProductInventory
                            {
                                //----اگر محصول از نوع واسطه باشد از مقدار خرید مقدار سفارش کم می شود
                                ApproximateInventory = (prodBrand.Warehouse.WarehouseTypeId == 2 ? prodBrand.ProximateAmount : 0) - prodBrand.ProximateAmount,
                                ProductBrandId = prodBrand.ProductBrandId,
                                OrderPoint = 0,
                                MinInventory = 0,
                                MaxInventory = 0,
                                IsActive = true,
                                FloorInventory = 0,
                                WarehouseId = wh.Id,
                                Created = DateTime.Now,
                                CreatedBy = Guid.Parse(_authenticatedUser.UserId),
                            });
                        }
                    }
                    else
                    {
                        if (prodBrand.WarehouseId == 3)
                        {
                            prodInventory.ApproximateInventory += prodBrand.ProximateAmount;
                        }
                        _productInventory.Update(prodInventory);

                        prodInventory.ApproximateInventory -= prodBrand.ProximateAmount;
                        if (order.InvoiceTypeId == 1 || order.InvoiceTypeId == 2)
                        {
                            var prodOfficialInventory = await _productInventory
                                .FirstOrDefaultAsync(i => i.ProductBrandId == prodBrand.ProductBrandId &&
                                            i.WarehouseId == prodBrand.WarehouseId);

                        }
                        _productInventory.Update(prodInventory);
                    }

                }
            }

            var newOrder = await _orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            if (order.Customer != null)
            {
                await _smsService.SendAsync(new SmsRequest
                {
                    Mobile = order.Customer.Mobile,
                    Message = $"مشتری گرامی \n سفارش شما به شماره {order.OrderCode} دریافت شد . \n  شرکت فولاد سپهر ایرانیان"
                });
            }

            return newOrder.Entity;

        }

        public async Task<bool> ApproveInvoiceType(Guid orderId)
        {
            var order = await _orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                throw new ApiException("سفارش یافت نشد !");
            }
            if (order.OrderStatusId == (int)OrderStatusEnum.AccApproved)//.IsApprovedInvoiceType)
                throw new ApiException("این سفارش قبلا تایید شده است !");

            //order.IsApprovedInvoiceType = true;
            order.OrderStatusId = (int)OrderStatusEnum.AccApproved;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ConfirmOrder(Guid orderId)
        {
            var order = await _orders.AsNoTracking().Include(o => o.Customer).FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                throw new ApiException("سفارش یافت نشد !");
            }
            if (order.OrderStatusId == (int)OrderStatusEnum.Confirmed)
                throw new ApiException("این سفارش قبلا تایید شده است !");

            order.OrderStatusId = (int)OrderStatusEnum.Confirmed;
            await _dbContext.SaveChangesAsync();

            await _smsService.SendAsync(new SmsRequest
            {
                Mobile = order.Customer.Mobile,
                Message = $"مشتری گرامی {string.Concat(order.Customer.FirstName, " ", order.Customer.LastName)} عزیز \n " +
                $"سفارش شما به شماره {order.OrderCode} تکمیل و تایید شد ." +
                "\n شرکت فولاد سپهر ایرانیان"
            });

            return true;
        }

        public async Task<IEnumerable<Order>> GetAllNotSendedOrders(GetNotAnnouncedOrdersParameter param)
        {
            return await _orders.AsNoTracking()
                .Where(o =>
                (o.OrderCode == param.OrderCode || param.OrderCode == null) &&
                o.OrderStatusId != (int)OrderStatusEnum.Sended &&
                            !new int[] { (int)OrderStatusEnum.Canceled, (int)OrderStatusEnum.ReturnedBack }.Contains(o.OrderStatusId))//!o.IsCompletlySended)
                .Include(c => c.Customer).ThenInclude(c => c.CustomerOfficialCompanies)
                .Include(o => o.OrderPayments)
                .Include(o => o.OrderServices).ThenInclude(s => s.Service)
                .Include(c => c.OrderSendType)
                .Include(c => c.OrderExitType)
                .Include(c => c.InvoiceType)
                .Include(c => c.FarePaymentType)
                .Include(c => c.CargoAnnounces).ThenInclude(c => c.CargoAnnounceDetails)
                .Include(c => c.CustomerOfficialCompany)
                .Include(o => o.Details).ThenInclude(d => d.AlternativeProductBrand).ThenInclude(i=>i.Brand)
                .Include(o => o.Details).ThenInclude(d => d.ProductBrand).ThenInclude(o => o.Brand)
                .Include(o => o.Details).ThenInclude(d => d.Product)
                .Include(o => o.Details).ThenInclude(d => d.Warehouse).ThenInclude(w => w.WarehouseType)
                .OrderByDescending(p => p.Created).ToListAsync();
        }

        public async Task<Order?> GetOrderById(Guid orderId)
        {
            return await _orders.AsNoTracking().Where(o => o.Id == orderId)
                .Include(o => o.OrderServices).ThenInclude(s => s.Service).AsNoTracking()
                .Include(o => o.OrderPayments)
                .Include(o => o.OrderStatus)
                .Include(c => c.Customer)
                .Include(c => c.OrderSendType)
                .Include(c => c.InvoiceType)
                .Include(c => c.Attachments)
                .Include(c => c.FarePaymentType)
                .Include(c => c.OrderExitType)
                .Include(c => c.CustomerOfficialCompany)
                .Include(o => o.Details).ThenInclude(o => o.CargoAnnounces)
                .Include(o => o.Details).ThenInclude(o => o.ProductSubUnit).AsNoTracking()
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Brand)
                .Include(o => o.Details).ThenInclude(d => d.PurchaseInvoiceType)
                .Include(o => o.Details).ThenInclude(d => d.PurchaserCustomer)
                .Include(o => o.Details).ThenInclude(d => d.AlternativeProductBrand).ThenInclude(i => i.Brand)
                .Include(o => o.Details).ThenInclude(d => d.ProductBrand).ThenInclude(o => o.Brand)
                .Include(o => o.Details).ThenInclude(d => d.Product).ThenInclude(o => o.ProductMainUnit)
                .Include(o => o.Details).ThenInclude(d => d.Product).ThenInclude(o => o.ProductSubUnit).AsNoTracking()
                .Include(o => o.CargoAnnounces).ThenInclude(c => c.CargoAnnounceDetails)
                .Include(o => o.Details).ThenInclude(d => d.Warehouse).ThenInclude(w => w.WarehouseType).FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Order>> GetAllOrdersAsync(GetAllOrdersParameter param)
        {
            var query = _orders.AsNoTracking()
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderServices).ThenInclude(s => s.Service)
                .Include(c => c.Customer)
                .Include(c => c.OrderStatus)
                .Include(o => o.OrderPayments)
                .Include(c => c.OrderSendType)
                .Include(c => c.InvoiceType)
                .Include(c => c.FarePaymentType)
                .Include(c => c.OrderExitType)
                .Include(c => c.CustomerOfficialCompany)
                .Include(o => o.Details).ThenInclude(d => d.AlternativeProductBrand).ThenInclude(i=>i.Brand)
                .Include(o => o.Details).ThenInclude(d => d.ProductBrand).ThenInclude(o => o.Brand)
                .Include(o => o.Details).ThenInclude(d => d.Product)
                .Include(o => o.Details).ThenInclude(d => d.Warehouse)
                .ThenInclude(w => w.WarehouseType)
                .AsQueryable();

            return query
                .Where(o =>
                (o.OrderCode == param.OrderCode || param.OrderCode == null) &&
                (o.OrderStatusId == param.OrderStatusId || param.OrderStatusId == null) &&
                (param.InvoiceTypeId.Count() == 0 || param.InvoiceTypeId.Contains(o.InvoiceTypeId)))
                .OrderByDescending(o => o.Created);
            //.Skip((param.PageNumber - 1) * param.PageSize)
            //.Take(param.PageSize)
            //.AsNoTracking()
            //.ToListAsync();
        }

        public Task<bool> IsUniqueBarcodeAsync(string barcode)
        {
            return _orders
                .AllAsync(p => p.Barcode != barcode);
        }

        public async Task<bool> ReturnOrder(Guid orderId)
        {
            var order = await _orders.AsNoTracking()
                .Include(o => o.LadingPermits.Where(x => x.IsActive))
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new ApiException("سفارش یافت نشد !");

            if (order.OrderStatusId != (int)OrderStatusEnum.Confirmed)
                throw new ApiException("سفارش تایید نشده است !");

            if (order.LadingPermits.Count() > 0)  //throw new ApiException("برای این سفارش ارسال بار ثبت شده است !");
                order.OrderStatusId = (int)OrderStatusEnum.ReturnedBack;
            else
                order.OrderStatusId = (int)OrderStatusEnum.Sended;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async void RemoveRelatedDetails(Guid id)
        {
            var order = await _orders.AsNoTracking()
                .Include(o => o.LadingPermits.Where(x => x.IsActive))
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new ApiException("سفارش یافت نشد !");

            if (order.OrderPayments != null)
                order.OrderPayments.Clear();
            if (order.Details != null)
                order.Details.Clear();
            //await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AddAttachmnets(List<Attachment> orderAttachments, Guid OrderId)
        {
            var query = "INSERT INTO sepdb.Attachment ([Id],[FileData],[OrderId],[IsActive])  " +
                        " VALUES (@Id,@FileData,@OrderId,@IsActive)";

            using (var connection = _dapContext.CreateConnection())
            {
                foreach (var item in orderAttachments)
                {
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Id", Guid.NewGuid());
                    dynamicParameters.Add("@FileData", item.FileData);
                    dynamicParameters.Add("@OrderId", OrderId);
                    dynamicParameters.Add("@IsActive", true);

                    var attachs = await connection.ExecuteAsync(query, dynamicParameters);

                    //throw new ApiException("خطا در بارگذاری فایل !");
                }
            }

            //var order = await _orders.FirstOrDefaultAsync(o => o.Id == OrderId);
            //if (order == null)
            //    throw new ApiException("سفارش یافت نشد !");

            //order.Attachments= orderAttachments;
            //_orders.Attach(order);

            return true;
        }

        public async Task<Order?> GetOrderInfo(long orderCode)
        {
            return await _orders.AsNoTracking().Where(o => o.OrderCode == orderCode)
                .Include(o => o.OrderServices).ThenInclude(s => s.Service)
                .Include(o => o.OrderPayments)
                .Include(o => o.OrderStatus)
                .Include(c => c.Customer)
                .Include(c => c.OrderSendType)
                .Include(c => c.InvoiceType)
                .Include(c => c.Attachments)
                .Include(c => c.FarePaymentType)
                .Include(c => c.OrderExitType)
                .Include(c => c.CustomerOfficialCompany)
                .Include(o => o.Details).ThenInclude(o => o.CargoAnnounces)
                .Include(o => o.Details).ThenInclude(o => o.ProductSubUnit)
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Brand)
                .Include(o => o.Details).ThenInclude(d => d.PurchaseInvoiceType)
                .Include(o => o.Details).ThenInclude(d => d.PurchaserCustomer)
                .Include(o => o.Details).ThenInclude(d => d.AlternativeProductBrand).ThenInclude(i => i.Brand)
                .Include(o => o.Details).ThenInclude(d => d.ProductBrand).ThenInclude(o => o.Brand)
                .Include(o => o.Details).ThenInclude(d => d.Product).ThenInclude(o => o.ProductMainUnit)
                .Include(o => o.Details).ThenInclude(d => d.Product).ThenInclude(o => o.ProductSubUnit)
                .Include(o => o.Details).ThenInclude(d => d.Warehouse).ThenInclude(w => w.WarehouseType).FirstOrDefaultAsync();
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            //var po =await _dbContext.Set<OrderDetail>()
            //    .FirstOrDefaultAsync(o => o.OrderId == order.Id && 
            //        o.Warehouse.WarehouseTypeId == 2 && o.Order.IsActive);
            //if (po!=null)
            //    throw new ApiException($"برای این سفارش یک سفارش خرید به شماره {po.o} ثبت شده است، و ابتدا باید تعیین تکلیف شود.");

            try
            {
                //var ord = await _dbContext.Orders
                //        .AsNoTracking()
                //        .Include(i => i.Details).ThenInclude(i => i.PurchaseOrder)
                //        .FirstOrDefaultAsync(o => o.Id == order.Id);
                if (order == null)
                    throw new ApiException("سفارش یافت نشد !");

                var toRemoveDetails = _orderDetail
                    .Where(s => s.OrderId == order.Id && s.WarehouseId != 3 &&
                    !order.Details.Select(d => d.Id).Contains(s.Id));

                foreach (var item in toRemoveDetails.Where(x => x.WarehouseId == 3))
                {
                    _dbContext.PurchaseOrder.Remove(item.PurchaseOrder);
                }

                _dbContext.OrderDetails.RemoveRange(toRemoveDetails);
                _dbContext.OrderServices.RemoveRange(_dbContext.OrderServices
                    .Where(s => s.OrderId == order.Id && !order.OrderServices.Select(d => d.Id).Contains(s.Id)));
                _dbContext.OrderPayments.RemoveRange(_dbContext.OrderPayments
                    .Where(s => s.OrderId == order.Id && !order.OrderPayments.Select(d => d.Id).Contains(s.Id)));

                foreach (var oitem in order.Details.Where(d => d.WarehouseId == 3))//.GroupBy(g=> new { g.ProductBrandId,g.Id}))
                {
                    _dbContext.PurchaseOrder.Remove(_dbContext.PurchaseOrder.First(p => p.Id == oitem.PurchaseOrderId));
                    var inv = await _dbContext.ProductInventories
                        .FirstOrDefaultAsync(x => x.ProductBrandId == oitem.ProductBrandId && x.WarehouseId == 3);

                    inv.ApproximateInventory -= oitem.ProximateAmount;
                }

                #region بررسی می شود که کالای مورد ویرایش اگر دارای بارگیری باشد، مقدار بارگیری شده از مقدار اصلی کمتر نباشد
                foreach (var oitem in order.Details.Where(d => d.Id != 0))
                {
                    var od = order.Details.FirstOrDefault(d => d.Id == oitem.Id);
                    if (od != null)
                    {
                        List<CargoAnnounceDetail> od_cAnncs =
                            await _dbContext.Set<CargoAnnounceDetail>().Where(a => a.OrderDetailId == od.Id).ToListAsync();

                        if (od.ProximateAmount < od_cAnncs.Sum(c => c.LadingAmount))
                            throw new ApiException(
                                string.Format(@"مقدار اصلی نمی تواند از مقدار بارگیری شده کمتر باشد !" + "({0} {1}) ",
                                od_cAnncs.First(d => d.OrderDetailId == od.Id).OrderDetail.ProductBrand.Product.ProductName,
                                od_cAnncs.First(d => d.OrderDetailId == od.Id).OrderDetail.ProductBrand.Brand.Name
                                ));
                    }
                }
                #endregion

                #region بروزرسانی موجودی
                if (order.OrderTypeId == OrderType.Urgant)
                {
                    foreach (var oitem in order.Details)
                    {
                        var prodInventory = await _dbContext.ProductInventories
                                        .FirstOrDefaultAsync(i => i.ProductBrandId == oitem.ProductBrandId &&
                                            i.WarehouseId == oitem.WarehouseId);

                        if (oitem.WarehouseId == 3)
                        {
                            prodInventory.ApproximateInventory += oitem.ProximateAmount;
                        }
                        _productInventory.Update(prodInventory);

                        //---اگه محصول ویرایش شده باشه ابتدا مقدار قبلی به موجودی اضافه شده و سپس از مقدار جدید کسر خواهد شد .

                        var prevProd = order.Details.FirstOrDefault(d => d.Id == oitem.ProductBrandId);
                        if (prodInventory != null)
                        {
                            prodInventory.ApproximateInventory = prevProd == null ?
                                prodInventory.ApproximateInventory - oitem.ProximateAmount :
                                prodInventory.ApproximateInventory + prevProd.ProximateAmount - oitem.ProximateAmount;

                            _productInventory.Update(prodInventory);
                        }
                    }
                }
                #endregion

                _orders.Update(order);
                await _dbContext.SaveChangesAsync();

                return order;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<bool> ApproveOrderInvoiceType(Order order)
        {
            _orders.Update(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Order?> GetOrderForUpdateInvoiceType(Guid orderId)
        {
            return await _orders.AsNoTracking().Where(o => o.Id == orderId)
                .Include(o => o.Details).FirstOrDefaultAsync();
        }
    }
}