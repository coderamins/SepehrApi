using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class DapperProduct
    {
        public Guid ProductId { get; set; }
        public long ProductCode { get; set; }
        public string? Barcode { get; set; }
        public required string ProductName { get; set; }
        public int? ProductTypeId { get; set; }
        public string ProductTypeDesc { get; set; }
        public decimal ProductPrice { get; set; }
        public required string ProductSize { get; set; }
        public string ProductThickness { get; set; } = string.Empty;
        public decimal ApproximateWeight { get; set; }
        public int NumberInPackage { get; set; }
        public int? ProductStandardId { get; set; }
        public int? ProductStateId { get; set; }
        public int ProductMainUnitId { get; set; }
        public string ProductMainUnitDesc { get; set; }
        public int? ProductSubUnitId { get; set; }
        public string ProductSubUnitDesc { get; set; }
        public double? ExchangeRate { get; set; }
        public int WarehouseTypeId { get; set; }
        public int InventotyCriticalPoint { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        public int BrandId { get; set; }

        //Product Inventory
        public int ProductBrandId { get; set; }
        public string ProductBrandName { get; set; }
        public decimal Thickness { get; set; }
        public int WarehouseId { get; set; }
        public double ApproximateInventory { get; set; }
        public double OnTransitInventory { get; set; }
        public double FloorInventory { get; set; }
        public double MaxInventory { get; set; }
        public double MinInventory { get; set; }
        public decimal PurchaseInventory { get; set; }
        //Warehose
        public string WarehouseName { get; set; }

        //Product State
        public string ProductStateDesc { get; set; }

        //Product Standard
        public string ProductStandardDesc { get; set; }
        public int TotalCount { get; set; }

    }
}
