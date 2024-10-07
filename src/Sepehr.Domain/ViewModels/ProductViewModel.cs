using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public long ProductCode { get; set; }
        public required string ProductName { get; set; }
        public int? ProductTypeId { get; set; }
        public string? ProductTypeDesc { get; set; }
        public string ProductSize { get; set; } = string.Empty;
        public string ProductThickness { get; set; } = string.Empty;
        public decimal ApproximateWeight { get; set; }
        public int NumberInPackage { get; set; }
        public int? ProductStandardId { get; set; }
        public string ProductStandardDesc { get; set; } = string.Empty;
        public int? ProductStateId { get; set; }
        public string ProductStateDesc { get; set; } = string.Empty;
        public int ProductMainUnitId { get; set; }
        public string ProductMainUnitDesc { get; set; } = string.Empty;
        public int? ProductSubUnitId { get; set; }
        public string ProductSubUnitDesc { get; set; } = string.Empty;
        public int ProductBrandId { get; set; }
        public string ProductBrandName { get; set; } = string.Empty;
        public int WarehouseTypeId { get; set; }
        public double OnTransitInventory { get; set; }
        public int MaxInventory { get; set; } //---حداکثر موجودی
        public int MinInventory { get; set; } //---حداقل موجودی
        public int InventotyCriticalPoint { get; set; } //---نقطه بحرانی
        public decimal ProximateWeightedAverage { get; set; }
        public decimal ActualWeightedAverage { get; set; }

        public string? Description { get; set; }
        public int StatusId { get; set; }
        public string? ProductIntegratedName { get; set; }
        public decimal ProductPrice { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public double Inventory { get; set; }
        public double ApproximateInventory { get; set; }
        public double FloorInventory { get; set; }
        public decimal PurchaseInventory { get; set; }
        public string WarehouseName { get; set; }
        public int WarehouseId { get; set; }
        public double? ExchangeRate { get; set; }
        public int TotalCount { get; set; }
        public decimal Rank { get; set; }

        public ProductWarehouseViewModel Warehouse { get; set; } = new ProductWarehouseViewModel();
        public List<ProductPriceViewModel> ProductPrices { get; set; } = new List<ProductPriceViewModel>();
        public List<ProductInventoryViewModel> ProductInventories { get; set; } = new List<ProductInventoryViewModel>();
        public List<ProductBrandViewModel> ProductBrands { get; set; } = new List<ProductBrandViewModel>();
        //public ProductDetailViewModel ProductDetail { get; set; }

    }
}