using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.DTOs.Email;
using Sepehr.Application.DTOs.Sms;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder;
using Sepehr.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;
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
    public class PurchaseOrderRepositoryAsync :
        GenericRepositoryAsync<PurchaseOrder>,
        IPurchaseOrderRepositoryAsync
    {
        private readonly DbSet<PurchaseOrder> _purchaseOrders;
        private readonly DbSet<PurchaseOrderDetail> _purchaseOrderDetail;
        private readonly DbSet<OrderService> _purchaseOrderServices;
        private readonly DbSet<OrderPayment> _purchaseOrderPayments;
        private readonly DbSet<ProductInventory> _productInventory;
        private readonly DbSet<Warehouse> _warehouses;
        private readonly DbSet<PurchaseOrderTransfer> _purchaseOrderTransfers;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly DapperContext _dapContext;

        public PurchaseOrderRepositoryAsync(
            ApplicationDbContext dbContext
            , DapperContext dapContext
            , IEmailService emailService
            , ISmsService smsService
            , IMapper mapper
            ) : base(dbContext)
        {
            _purchaseOrderPayments = dbContext.Set<OrderPayment>();
            _purchaseOrderServices = dbContext.Set<OrderService>();
            _purchaseOrderDetail = dbContext.Set<PurchaseOrderDetail>();
            _purchaseOrders = dbContext.Set<PurchaseOrder>();
            _purchaseOrderTransfers = dbContext.Set<PurchaseOrderTransfer>();
            _warehouses = dbContext.Set<Warehouse>();
            _emailService = emailService;
            _smsService = smsService;
            _dbContext = dbContext;
            _dapContext = dapContext;
            _productInventory = dbContext.Set<ProductInventory>();
            _mapper = mapper;
        }

        public async Task<PurchaseOrder> CreateOrder(PurchaseOrder order)
        {
            try
            {
                //-----با ثبت سفارش خرید یه موجودی انبار خرید مبدا کسر میگردد-----
                foreach (var prodBrand in order.Details)
                {
                    var prodInventory = await _productInventory
                            .FirstOrDefaultAsync(i =>
                            i.ProductBrandId == prodBrand.ProductBrandId &&
                            i.WarehouseId == order.OriginWarehouseId);

                    if (prodInventory == null)
                    {
                        await _productInventory.AddAsync(new ProductInventory
                        {
                            PurchaseInventory = prodBrand.ProximateAmount,
                            ProductBrandId = prodBrand.ProductBrandId,
                            OrderPoint = 0,
                            MinInventory = 0,
                            MaxInventory = 0,
                            FloorInventory = 0,
                            WarehouseId = order.OriginWarehouseId,
                            IsActive = true,
                        });
                    }
                    else
                    {
                        prodInventory.PurchaseInventory += prodBrand.ProximateAmount;
                        _productInventory.Update(prodInventory);
                    }
                }

                var newOrder = await _dbContext
                    .AddAsync<PurchaseOrder>(order);

                await _dbContext.SaveChangesAsync();

                return newOrder.Entity;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<PurchaseOrder>> CreateOrderForIntermediatProducts(List<PurchaseOrder> orders)
        {
            try
            {
                foreach (var ord in orders)
                {
                    //-----با ثبت سفارش خرید موجودی انبار واسطه اضافه میشه-----
                    foreach (var prodBrand in ord.Details)
                    {
                        var prodInventory = await _productInventory
                                .FirstOrDefaultAsync(i =>
                                i.ProductBrandId == prodBrand.ProductBrandId &&
                                i.Warehouse.WarehouseTypeId == 2
                                );

                        if (prodInventory == null)
                        {
                            await _productInventory.AddAsync(new ProductInventory
                            {
                                PurchaseInventory = prodBrand.ProximateAmount,
                                ProductBrandId = prodBrand.ProductBrandId,
                                OrderPoint = 0,
                                MinInventory = 0,
                                MaxInventory = 0,
                                FloorInventory = 0,
                                WarehouseId = 3,
                                IsActive = true,
                            });
                        }
                        else
                        {
                            prodInventory.PurchaseInventory += prodBrand.ProximateAmount;
                            _productInventory.Update(prodInventory);
                        }
                    }

                    var newOrder = await _dbContext
                        .AddAsync<PurchaseOrder>(ord);

                    await _dbContext.SaveChangesAsync();
                }

                return null;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<bool> ApproveInvoiceType(Guid purchaseOrderId)
        {
            var purchaseOrder = await _purchaseOrders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == purchaseOrderId);
            if (purchaseOrder == null)
            {
                throw new ApiException("سفارش یافت نشد !");
            }
            if (purchaseOrder.OrderStatusId == (int)PurchaseOrderStatusEnum.AccApproved)
                throw new ApiException("این سفارش قبلا تایید شده است !");

            purchaseOrder.OrderStatusId = (int)PurchaseOrderStatusEnum.AccApproved;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ConfirmPurchaseOrder(Guid purchaseOrderId)
        {
            var purchaseOrder = await _purchaseOrders.AsNoTracking().Include(o => o.Customer).FirstOrDefaultAsync(o => o.Id == purchaseOrderId);
            if (purchaseOrder == null)
            {
                throw new ApiException("سفارش یافت نشد !");
            }
            if (purchaseOrder.OrderCode == (int)PurchaseOrderStatusEnum.AccApproved)
                throw new ApiException("این سفارش قبلا تایید شده است !");

            purchaseOrder.OrderStatusId = (int)PurchaseOrderStatusEnum.AccApproved;
            await _dbContext.SaveChangesAsync();

            await _smsService.SendAsync(new SmsRequest
            {
                Mobile = purchaseOrder.Customer.Mobile,
                Message = $"مشتری گرامی {string.Concat(purchaseOrder.Customer.FirstName, " ", purchaseOrder.Customer.LastName)} عزیز \n " +
                $"سفارش شما به شماره {purchaseOrder.OrderCode} تکمیل و تایید شد ." +
                "\n شرکت فولاد سپهر ایرانیان"
            });

            return true;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllNotSendedPurchaseOrders()
        {
            return await _purchaseOrders.AsNoTracking()//.Where(o => o.OrderStatusId == (int)PurchaseOrderStatusEnum.Sended)
                .Include(o => o.CustomerOfficialCompany)
                .Include(o => o.Customer).ThenInclude(c => c.CustomerOfficialCompanies)
                .Include(o => o.OrderPayments)
                .Include(o => o.OrderServices).ThenInclude(s => s.Service)
                .Include(o => o.OrderSendType)
                .Include(o => o.InvoiceType)
                .Include(o => o.FarePaymentType)
                .Include(o => o.OriginWarehouse)
                .Include(o => o.DestinationWarehouse)
                .Include(o => o.Details).ThenInclude(d => d.AlternativeProductBrand)
                .Include(o => o.Details).ThenInclude(d => d.ProductBrand).ThenInclude(o => o.Brand)
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Product).ThenInclude(o => o.ProductMainUnit)
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Product).ThenInclude(o => o.ProductSubUnit)
                //.Include(o => o.Details).ThenInclude(d => d.Warehouse).ThenInclude(w => w.WarehouseType)
                .OrderByDescending(p => p.Created).ToListAsync();
        }

        public async Task<PurchaseOrder?> GetPurchaseOrderById(Guid purchaseOrderId)
        {
            return await _purchaseOrders.AsNoTracking().Where(o => o.Id == purchaseOrderId)
                .Include(c => c.CustomerOfficialCompany).AsNoTracking()
                .Include(o => o.OrderServices).ThenInclude(s => s.Service).AsNoTracking()
                .Include(o => o.OrderPayments).AsNoTracking()
                .Include(o => o.OrderStatus).AsNoTracking()
                .Include(c => c.Customer).AsNoTracking()
                .Include(c => c.OrderSendType).AsNoTracking()
                .Include(c => c.InvoiceType).AsNoTracking()
                .Include(c => c.Attachments).AsNoTracking()
                .Include(c => c.FarePaymentType).AsNoTracking()
                .Include(c => c.OriginWarehouse)
                .Include(c => c.DestinationWarehouse)
                .Include(o => o.Details).ThenInclude(o => o.ProductSubUnit).AsNoTracking()
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Brand).AsNoTracking()
                .Include(o => o.Details).ThenInclude(d => d.PurchaseInvoiceType).AsNoTracking()
                .Include(o => o.Details).ThenInclude(d => d.PurchaserCustomer).AsNoTracking()
                .Include(o => o.Details).ThenInclude(d => d.AlternativeProductBrand).AsNoTracking()
                .Include(o => o.Details).ThenInclude(d => d.ProductBrand).ThenInclude(o => o.Brand).AsNoTracking()
                .Include(o => o.Details).ThenInclude(o => o.ProductSubUnit).AsNoTracking()
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Product).ThenInclude(o => o.ProductMainUnit).AsNoTracking()
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Product).ThenInclude(o => o.ProductSubUnit).AsNoTracking()
                //.Include(o => o.Details).ThenInclude(d => d.Warehouse).ThenInclude(w => w.WarehouseType)
                .AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<PurchaseOrder?> moGetPurchaseOrderByIdAsQueryble(Guid purchaseOrderId)
        {
            return await _purchaseOrders.Include(o => o.Details)
                .AsQueryable().AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == purchaseOrderId);
        }

        public async Task<IQueryable<PurchaseOrder>> GetAllPurchaseOrdersAsync(GetAllPurchaseOrdersParameter param)
        {
            var query = _purchaseOrders.AsNoTracking()
                .Include(c => c.CustomerOfficialCompany)
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderServices).ThenInclude(s => s.Service)
                .Include(c => c.Customer)
                .Include(o => o.OrderPayments)
                .Include(c => c.OrderSendType)
                .Include(c => c.InvoiceType)
                .Include(c => c.FarePaymentType)
                .Include(c => c.OriginWarehouse)
                .Include(c => c.DestinationWarehouse)
                .Include(o => o.Details).ThenInclude(d => d.AlternativeProductBrand)
                .Include(o => o.Details).ThenInclude(d => d.ProductBrand).ThenInclude(o => o.Brand)
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Product).ThenInclude(o => o.ProductMainUnit)
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Product).ThenInclude(o => o.ProductSubUnit)
                //.Include(o => o.Details).ThenInclude(d => d.Warehouse).ThenInclude(w => w.WarehouseType)
                .AsQueryable();

            return query
                .Where(o =>
                //(o.OrderStatusId != 4 && param.IsNotTransferedToWarehouse == true || param.IsNotTransferedToWarehouse == null) &&
                (o.OrderCode == param.OrderCode || param.OrderCode == null) &&
                (o.OrderStatusId == param.PurchaseOrderStatusId || param.PurchaseOrderStatusId == null) &&
                (param.InvoiceTypeId.Count() == 0 || param.InvoiceTypeId.Contains(o.InvoiceTypeId)))
                .OrderByDescending(o => o.Created);
            //.Skip((param.PageNumber - 1) * param.PageSize)
            //.Take(param.PageSize)
            //.AsNoTracking()
            //.ToListAsync();
        }

        public Task<bool> IsUniqueBarcodeAsync(string barcode)
        {
            return _purchaseOrders
                .AllAsync(p => p.Barcode != barcode);
        }

        public async Task<bool> ReturnPurchaseOrder(Guid purchaseOrderId)
        {
            var purchaseOrder = await _purchaseOrders.AsNoTracking()
                .Include(o => o.LadingLicenses)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == purchaseOrderId);

            if (purchaseOrder == null)
                throw new ApiException("سفارش یافت نشد !");

            if (purchaseOrder.OrderStatusId != (int)OrderStatusEnum.AccApproved)
                throw new ApiException("سفارش تایید نشده است !");


            purchaseOrder.OrderStatusId = (int)PurchaseOrderStatusEnum.ReturnedBack;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async void RemoveRelatedDetails(Guid id)
        {
            var purchaseOrder = await _purchaseOrders.AsNoTracking()
                .Include(o => o.LadingLicenses)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (purchaseOrder == null)
                throw new ApiException("سفارش یافت نشد !");

            if (purchaseOrder.OrderPayments != null)
                purchaseOrder.OrderPayments.Clear();
            if (purchaseOrder.Details != null)
                purchaseOrder.Details.Clear();
            //await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AddAttachmnets(List<Attachment> purchaseOrderAttachments, Guid PurchaseOrderId)
        {
            var query = "INSERT INTO sepdb.Attachment ([Id],[FileData],[PurchaseOrderId],[IsActive])  " +
                        " VALUES (@Id,@FileData,@PurchaseOrderId,@IsActive)";

            using (var connection = _dapContext.CreateConnection())
            {
                foreach (var item in purchaseOrderAttachments)
                {
                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Id", Guid.NewGuid());
                    dynamicParameters.Add("@FileData", item.FileData);
                    dynamicParameters.Add("@PurchaseOrderId", PurchaseOrderId);
                    dynamicParameters.Add("@IsActive", true);

                    var attachs = await connection.ExecuteAsync(query, dynamicParameters);

                    //throw new ApiException("خطا در بارگذاری فایل !");
                }
            }

            //var purchaseOrder = await _purchaseOrders.FirstOrDefaultAsync(o => o.Id == PurchaseOrderId);
            //if (purchaseOrder == null)
            //    throw new ApiException("سفارش یافت نشد !");

            //purchaseOrder.Attachments= purchaseOrderAttachments;
            //_purchaseOrders.Attach(purchaseOrder);

            return true;
        }

        public async Task<PurchaseOrder?> GetPurchaseOrderInfo(long OrderCode)
        {
            return await _purchaseOrders.AsNoTracking().Where(o => o.OrderCode == OrderCode)
                .Include(c => c.CustomerOfficialCompany)
                .Include(o => o.OrderServices).ThenInclude(s => s.Service)
                .Include(o => o.OrderPayments)
                .Include(o => o.OrderStatus)
                .Include(c => c.Customer)
                .Include(c => c.OrderSendType)
                .Include(c => c.InvoiceType)
                .Include(c => c.Attachments)
                .Include(c => c.OriginWarehouse)
                .Include(c => c.DestinationWarehouse)
                .Include(c => c.FarePaymentType)
                .Include(o => o.Details).ThenInclude(o => o.ProductSubUnit)
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Brand)
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Product).ThenInclude(o => o.ProductMainUnit)
                .Include(o => o.Details).ThenInclude(o => o.ProductBrand).ThenInclude(o => o.Product).ThenInclude(o => o.ProductSubUnit)
                .Include(o => o.Details).ThenInclude(d => d.PurchaseInvoiceType)
                .Include(o => o.Details).ThenInclude(d => d.PurchaserCustomer)
                .Include(o => o.Details).ThenInclude(d => d.AlternativeProductBrand)
                .Include(o => o.Details).ThenInclude(d => d.ProductBrand).ThenInclude(o => o.Brand)
                .Include(o => o.Details).ThenInclude(d => d.ProductSubUnit)
                //.Include(o => o.Details).ThenInclude(d => d.Warehouse).ThenInclude(w => w.WarehouseType)
                .FirstOrDefaultAsync();
        }

        public async Task<PurchaseOrder> UpdatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _dbContext.ChangeTracker.AcceptAllChanges();

            var order = await _purchaseOrders
                .AsNoTracking()
                .Include(o => o.Details).AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == purchaseOrder.Id);

            _dbContext.PurchaseOrderDetails.RemoveRange(_dbContext.PurchaseOrderDetails.Where(s => s.OrderId == purchaseOrder.Id && !purchaseOrder.Details.Select(d => d.Id).Contains(s.Id)));//.Remove(os);
            _dbContext.PurchaseOrderServices.RemoveRange(_dbContext.PurchaseOrderServices.Where(s => s.OrderId == purchaseOrder.Id && !purchaseOrder.OrderServices.Select(d => d.Id).Contains(s.Id)));//.Remove(os);
            _dbContext.PurchaseOrderPayments.RemoveRange(_dbContext.PurchaseOrderPayments.Where(s => s.OrderId == purchaseOrder.Id && !purchaseOrder.OrderPayments.Select(d => d.Id).Contains(s.Id)));//.Remove(os);

            if (order == null)
                throw new ApiException("سفارش خرید یافت نشد !");

            if (order.OrderStatusId != (int)PurchaseOrderStatusEnum.NewOrder)
                throw new ApiException("وضعیت سفارش نامعتبر می باشد !");


            //--- ایجاد و بروزرسانی موجودی انبار مجازی
            foreach (var prodBrand in purchaseOrder.Details)
            {
                var prodInventory = await _productInventory.AsNoTracking().FirstOrDefaultAsync(i =>
                        i.ProductBrandId == prodBrand.ProductBrandId &&
                        i.WarehouseId == (int)EWarehouses.PurchaseWarehouse);

                if (prodInventory == null)
                {
                    await _dbContext.ProductInventories.AddAsync(new ProductInventory
                    {
                        ApproximateInventory = prodBrand.ProximateAmount,
                        ProductBrandId = prodBrand.ProductBrandId,
                        OrderPoint = 0,
                        MinInventory = 0,
                        MaxInventory = 0,
                        FloorInventory = 0,
                        WarehouseId = (int)EWarehouses.PurchaseWarehouse,
                        IsActive = true,
                    });
                }
                else
                {
                    var prevDetail = order.Details.FirstOrDefault(o => o.ProductBrandId == prodBrand.ProductBrandId);
                    if (prevDetail != null)
                        prodInventory.ApproximateInventory -= prevDetail.ProximateAmount;

                    prodInventory.ApproximateInventory += prodBrand.ProximateAmount;
                    _productInventory.Update(prodInventory);
                }
            }

            _purchaseOrders.Update(purchaseOrder);
            await _dbContext.SaveChangesAsync();

            return purchaseOrder;
        }

        public async Task<bool> DeletePurchaseOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CancelPurchaseOrder(Guid orderId)
        {
            var order = await _purchaseOrders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new ApiException("سفارش خرید یافت نشد !");

            if (order.OrderStatusId != (int)PurchaseOrderStatusEnum.NewOrder)
                throw new ApiException("وضعیت سفارش نامعتبر می باشد !");

            foreach (var prodBrand in order.Details)
            {
                var prodInventory = await _productInventory
                        .FirstOrDefaultAsync(i =>
                        i.ProductBrandId == prodBrand.ProductBrandId &&
                        i.WarehouseId == (int)EWarehouses.PurchaseWarehouse);

                if (prodInventory != null)
                {
                    prodInventory.ApproximateInventory -= prodBrand.ProximateAmount;
                    _productInventory.Update(prodInventory);
                }
            }

            order.OrderStatusId = (int)PurchaseOrderStatusEnum.Canceled;//--ابطال شده

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdatePurchaseOrderDetail(ICollection<PurchaseOrderDetail> details)
        {
            foreach (var prodBrand in details)
            {
                var prodInventory = await _productInventory
                        .FirstOrDefaultAsync(i =>
                        i.ProductBrandId == prodBrand.ProductBrandId &&
                        i.WarehouseId == (int)EWarehouses.PurchaseWarehouse);

                if (prodInventory != null)
                {
                    prodInventory.ApproximateInventory -= prodBrand.ProximateAmount;
                    _productInventory.Update(prodInventory);
                }
            }
            _purchaseOrderDetail.UpdateRange(details);
        }

        public Task UpdatePurchaseOrderDetail(ICollection<PurchaseOrderDetail> details, int warehouseId)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseOrder?> GetPurchaseOrderByIdAsQueryble(Guid purchaseOrderId)
        {
            throw new NotImplementedException();
        }

        public async Task<PurchaseOrderTransfer> TranserPurchaseOrder(TransferPurchaseOrderCommand command)
        {
            var purchaseOrder = await _purchaseOrders.AsNoTracking()
                .Include(o => o.Details).AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == command.OrderId);

            if (purchaseOrder == null)
                throw new ApiException($"سفارش یاقت نشد !");
            else if (purchaseOrder.OrderStatusId == (int)PurchaseOrderStatusEnum.TransferedToWarehouse)
                throw new ApiException("سفارش بصورت کامل انتقال داده شده !");
            else
            {
                var warehouse = await _warehouses.AsNoTracking()
                    .FirstOrDefaultAsync(w => w.Id == command.WarehouseId);
                if (warehouse == null || warehouse.WarehouseTypeId != 4)
                    throw new ApiException("انبار یافت نشد !");

                foreach (var item in command.TransferDetails)
                {
                    var poDetail = purchaseOrder.Details.First(od => od.Id == item.PurchaseOrderDetailId);
                    if (item.TransferedAmount + poDetail.TransferedAmount > poDetail.ProximateAmount)
                        throw new ApiException("مقدار انتقال برای کالابرند {0} نامعتبر می باشد !", item.ProductBrandName);

                    //---اضافه کردن مقدار انتقال داده شده در جدول خرید---
                    poDetail.TransferedAmount += item.TransferedAmount;

                    //------کسر از انبار خرید-------
                    var purchaseInventory = await _productInventory
                       .FirstOrDefaultAsync(i =>
                       i.ProductBrandId == poDetail.ProductBrandId &&
                       i.WarehouseId == (int)EWarehouses.PurchaseWarehouse);

                    if (purchaseInventory != null)
                    {
                        purchaseInventory.ApproximateInventory -= poDetail.ProximateAmount;
                        _productInventory.Update(purchaseInventory);
                    }

                    //------اضافه کردن به موجودی انبار انتخاب شده-------
                    var selectedWarehouseInventory = await _productInventory
                       .FirstOrDefaultAsync(i =>
                       i.ProductBrandId == poDetail.ProductBrandId &&
                       i.WarehouseId == command.WarehouseId);

                    if (selectedWarehouseInventory == null)
                    {
                        await _productInventory.AddAsync(new ProductInventory
                        {
                            ApproximateInventory = item.TransferedAmount,
                            ProductBrandId = poDetail.ProductBrandId,
                            OrderPoint = 0,
                            MinInventory = 0,
                            MaxInventory = 0,
                            FloorInventory = 0,
                            WarehouseId = command.WarehouseId,
                            IsActive = true,
                        });
                    }
                    else
                        selectedWarehouseInventory.ApproximateInventory += item.TransferedAmount;
                }

                //---بررسی میکنیم که اگر مقادیر همه کالاها به انبار انتقال داده شده باشند وضعیت سفارش هم به انتقال به انبار شده تغییر داده شود
                if (!purchaseOrder.Details.Any(d => d.TransferedAmount != d.ProximateAmount))
                {
                    purchaseOrder.OrderStatusId = (int)PurchaseOrderStatusEnum.TransferedToWarehouse;//---تکمیل انتقال به انبار داده شده
                }

                _purchaseOrders.Update(purchaseOrder);

                //----------ثبت انتقال سفارش خرید---------
                var purchaseOrderTransfer = _mapper.Map<PurchaseOrderTransfer>(command);
                var newPurchaseOrderTransfer = await _purchaseOrderTransfers.AddAsync(purchaseOrderTransfer);

                await _dbContext.SaveChangesAsync();
                return newPurchaseOrderTransfer.Entity;
            }
        }

    }
}