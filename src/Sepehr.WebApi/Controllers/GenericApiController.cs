using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sepehr.Domain.Entities;
using Sepehr.Application.Helpers;
using Sepehr.Infrastructure.Persistence.Context;
using Sepehr.Infrastructure.Persistence.Seeds;             

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class GenericApiController : BaseApiController
    {
        private readonly DbSet<PaymentRequestReason> _paymentRequestReason;
        private readonly DbSet<PaymentRequestStatus> _paymentRequestStatus;
        private readonly DbSet<OrderSendType> _orderSendTypes;
        private readonly DbSet<PurchaseOrderSendType> _purOrderSendTypes;
        private readonly DbSet<FarePaymentType> _orderFarePayments;
        private readonly DbSet<PurchaseOrderFarePaymentType> _purOrderFarePayments;
        private readonly DbSet<InvoiceType> _invoiceTypes;
        private readonly DbSet<CustomerValidity> _customerValidities;
        private readonly DbSet<WarehouseType> _warehouseTypes;
        private readonly DbSet<Warehouse> _warehouse;
        private readonly DbSet<PaymentOriginType> _receivePaymentTypes;
        private readonly DbSet<ProductUnit> _productUnits;
        private readonly DbSet<Service> _services;
        private readonly DbSet<ProductType> _productTypes;
        private readonly DbSet<OrderStatus> _orderStatus;
        private readonly DbSet<VehicleType> _vehicleTypes;
        private readonly DbSet<Bank> _banks;
        private readonly DbSet<PhoneNumberType> _phoneNumberTypes;
        private readonly DbSet<OrderExitType> _orderExitTypes;
        private readonly DbSet<CustomerLabelType> _customerLabelType;
        private readonly DbSet<TransferRemittanceStatus> _transferRemittanceStatuses;

        public GenericApiController(ApplicationDbContext dbContext)
        {
            _transferRemittanceStatuses=dbContext.Set<TransferRemittanceStatus>();
            _orderSendTypes = dbContext.Set<OrderSendType>();
            _purOrderSendTypes = dbContext.Set<PurchaseOrderSendType>();
            _orderFarePayments = dbContext.Set<FarePaymentType>();
            _purOrderFarePayments = dbContext.Set<PurchaseOrderFarePaymentType>();
            _invoiceTypes      = dbContext.Set<InvoiceType>();
            _customerValidities= dbContext.Set<CustomerValidity>();
            _warehouseTypes = dbContext.Set<WarehouseType>();
            _warehouse = dbContext.Set<Warehouse>();
            _receivePaymentTypes = dbContext.Set<PaymentOriginType>();
            _productUnits = dbContext.Set<ProductUnit>();
            _services = dbContext.Set<Service>();
            _productTypes = dbContext.Set<ProductType>();
            _orderStatus = dbContext.Set<OrderStatus>();
            _vehicleTypes = dbContext.Set<VehicleType>();
            _orderExitTypes = dbContext.Set<OrderExitType>();
            _phoneNumberTypes = dbContext.Set<PhoneNumberType>();
            _customerLabelType = dbContext.Set<CustomerLabelType>();
            _paymentRequestReason = dbContext.Set<PaymentRequestReason>();
            _paymentRequestStatus = dbContext.Set<PaymentRequestStatus>();
            _banks = dbContext.Set<Bank>();
        }

        //[HasPermission("GetOrderSendTypes")]
        [HttpGet("GetOrderSendTypes")]
        public async Task<IActionResult> GetOrderSendTypes()
        {
            return Ok(await _orderSendTypes.ToListAsync());
        }

        //[HasPermission("GetPurchaseOrderSendTypes")]
        [HttpGet("GetPurchaseOrderSendTypes")]
        public async Task<IActionResult> GetPurchaseOrderSendTypes()
        {
            return Ok(await _purOrderSendTypes.ToListAsync());
        }

        //[HasPermission("GetRentPaymentTypes")]
        [HttpGet("GetRentPaymentTypes")]
        public async Task<IActionResult> GetRentPaymentTypes()
        {
            return Ok(await _orderFarePayments.ToListAsync());
        }

        //[HasPermission("GetPurchaseFarePaymentTypes")]
        [HttpGet("GetPurchaseFarePaymentTypes")]
        public async Task<IActionResult> GetPurchaseFarePaymentTypes()
        {
            return Ok(await _purOrderFarePayments.ToListAsync());
        }

        //[HasPermission("GetInvoiceTypes")]
        [HttpGet("GetInvoiceTypes")]
        public async Task<IActionResult> GetInvoiceTypes()
        {
            return Ok(await _invoiceTypes.ToListAsync());
        }

        //[HasPermission("GetCustomerValidities")]
        [HttpGet("GetCustomerValidities")]
        public async Task<IActionResult> GetCustomerValidities()
        {
            return Ok(await _customerValidities.ToListAsync());
        }

        //[HasPermission("GetWarehouseTypes")]
        [HttpGet("GetWarehouseTypes")]
        public async Task<IActionResult> GetWarehouseTypes()
        {
            return Ok(await _warehouseTypes.ToListAsync());
        }

        //[HasPermission("GetWarehouses")]
        [HttpGet("GetWarehouses")]
        public async Task<IActionResult> GetWarehouses()
        {
            return Ok(await _warehouse.ToListAsync());
        }

        //[HasPermission("GetReceivePaymentSources")]
        [HttpGet("GetReceivePaymentSources")]
        public async Task<IActionResult> GetReceivePaymentSources()
        {
            return Ok(await _receivePaymentTypes.ToListAsync());
        }

        //[HasPermission("GetProductUnits")]
        [HttpGet("GetProductUnits")]
        public async Task<IActionResult> GetProductUnits()
        {
            return Ok(await _productUnits.ToListAsync());
        }

        //[HasPermission("GetServices")]
        [HttpGet("GetServices")]
        public async Task<IActionResult> GetServices()
        {
            return Ok(await _services.ToListAsync());
        }

        //[HasPermission("GetProductTypes")]
        [HttpGet("GetProductTypes")]
        public async Task<IActionResult> GetProductTypes()
        {
            return Ok(await _productTypes.ToListAsync());
        }

        //[HasPermission("GetOrderStatuses")]
        [HttpGet("GetOrderStatuses")]
        public async Task<IActionResult> GetOrderStatuses()
        {
            return Ok(await _orderStatus.ToListAsync());
        }

        //[HasPermission("GetVehicleTypes")]
        [HttpGet("GetVehicleTypes")]
        public async Task<IActionResult> GetVehicleTypes()
        {
            return Ok(await _vehicleTypes.ToListAsync());
        }

        //[HasPermission("GetAllTransferRemittanceStatus")]
        [HttpGet("GetAllTransferRemittanceStatus")]
        public async Task<IActionResult> GetAllTransferRemittanceStatus()
        {
            return Ok(await _transferRemittanceStatuses.ToListAsync());
        }

        //[HasPermission("GetAllBanks")]
        [HttpGet("GetAllBanks")]
        public async Task<IActionResult> GetAllBanks()
        {
            return Ok(await _banks.ToListAsync());
        }

        //[HasPermission("GetOrderExitTypes")]
        [HttpGet("GetOrderExitTypes")]
        public async Task<IActionResult> GetOrderExitTypes()
        {
            return Ok(await _orderExitTypes.ToListAsync());
        }

        //[HasPermission("GetPhoneNumberTypes")]
        [HttpGet("GetPhoneNumberTypes")]
        public async Task<IActionResult> GetPhoneNumberTypes()
        {
            return Ok(await _phoneNumberTypes.ToListAsync());
        }

        //[HasPermission("GetCustomerLabelTypes")]
        [HttpGet("GetCustomerLabelTypes")]
        public async Task<IActionResult> GetCustomerLabelTypes()
        {
            return Ok(await _customerLabelType.ToListAsync());
        }

        //[HasPermission("GetAllPaymentRequestReasons")]
        [HttpGet("GetAllPaymentRequestReasons")]
        public async Task<IActionResult> GetAllPaymentRequestReasons()
        {
            return Ok(await _paymentRequestReason.ToListAsync());
        }

        //[HasPermission("GetAllPaymentRequestStatus")]
        [HttpGet("GetAllPaymentRequestStatus")]
        public async Task<IActionResult> GetAllPaymentRequestStatus()
        {
            return Ok(await _paymentRequestStatus.ToListAsync());
        }



    }
}
