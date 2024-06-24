using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string BrandName { get; set; }
        public string? WarehouseName { get; set; }
        public decimal ProximateAmount { get; set; }
        public int NumberInPackage { get; set; }
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
        public int PurchaseInvoiceTypeId { get; set; }
        public Guid? PurchaserCustomerId { get; set; }
        public int PurchaserInvoiceTypeDesc { get; set; }
        public string? PurchaseInvoiceTypeDesc { get; set; }
        public string? PurchaserCustomerName { get; set; }
        public string? SellerCompanyRow { get; set; }
        public string? PurchaseSettlementDate { get; set; }
        public required string CargoSendDate { get; set; }
        public int ProductBrandId { get; set; }
        public int ProductSubUnitId { get; set; }
        public string ProductSubUnitDesc { get; set; }=string.Empty;
        public decimal ProductSubUnitAmount { get; set; }

        public int? AlternativeProductBrandId { get; set; }
        public string AlternativeProductBrandName { get; set; }
        public decimal AlternativeProductAmount { get; set; }
        public decimal AlternativeProductPrice { get; set; }
        public string? Description { get; set; }
        public int BrandId { get; set; }

        #region جهت مجوز بارگیری
        public  decimal RemainingAmountToLadingLicence { get; set; }

        #endregion

        public decimal TotalLoadedAmount { get; set; }
        public decimal RemainingLadingAmount { get; set; }

        public ProductViewModel Product { get; set; }
        public CustomerViewModel? PurchaserCustomer { get; set; }

    }
}
