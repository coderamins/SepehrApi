using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Order
{
    public class OrderDetailRequest
    {
        public int? Id { get; set; }
        public int RowId { get; set; }
        public Guid ProductId { get; set; }
        public int WarehouseId { get; set; }
        public decimal ProximateAmount { get; set; }
        public int NumberInPackage { get; set; }
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
        public required int ProductBrandId { get; set; }
        public int ProductSubUnitId { get; set; }
        public decimal ProductSubUnitAmount { get; set; }

        public int? PurchaseInvoiceTypeId { get; set; } = null;
        public Guid? PurchaserCustomerId { get; set; } = Guid.Empty;
        public string? SellerCompanyRow { get; set; }
        public string? Description { get; set; }

        public string? PurchaseSettlementDate { get; set; } = string.Empty;
        public required string CargoSendDate { get; set; }


    }

    public class UpdateOrderDetailRequest1
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public Guid ProductId { get; set; }
        public int WarehouseId { get; set; }
        public decimal ProximateAmount { get; set; }
        public int NumberInPackage { get; set; }
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
        public required int ProductBrandId { get; set; }
        public int ProductSubUnitId { get; set; }
        public decimal ProductSubUnitAmount { get; set; }

        public int? PurchaseInvoiceTypeId { get; set; } = null;
        public Guid? PurchaserCustomerId { get; set; } = Guid.Empty;
        public string? SellerCompanyRow { get; set; }
        public string? Description { get; set; }

        public string? PurchaseSettlementDate { get; set; } = string.Empty;
        public required string CargoSendDate { get; set; }

    }

}
