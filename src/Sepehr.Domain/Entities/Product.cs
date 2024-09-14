using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sepehr.Domain.Common;

namespace Sepehr.Domain.Entities
{
    public class Product : AuditableBaseEntity<Guid>
    {
        public int ProductCode { get; set; }
        public string? Barcode { get; set; }
        public required string ProductName { get; set; }
        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public required string ProductSize { get; set; }
        public string ProductThickness { get; set; } = string.Empty;
        public decimal ApproximateWeight { get; set; }
        public int NumberInPackage { get; set; }
        public int? ProductStandardId { get; set; }
        public int? ProductStateId { get; set; }
        [ForeignKey("ProductMainUnit")]
        public int ProductMainUnitId { get; set; }
        [ForeignKey("ProductSubUnit")]
        public int? ProductSubUnitId { get; set; }
        public double? ExchangeRate { get; set; }

        //#region جهت اعلان های مختلف موجودی به کاربر 
        //public int MaxInventory { get; set; }//---حداکثر موجودی
        //public int MinInventory { get; set; }//---حداقل موجودی
        //public int InventotyCriticalPoint { get; set; }//---نقطه بحرانی
        //#endregion

        public string? Description { get; set; }


        public virtual required ProductUnit ProductMainUnit { get; set; }
        public virtual ProductUnit? ProductSubUnit { get; set; }
        public required virtual ProductState? ProductState { get; set; }
        public required virtual ProductStandard? ProductStandard { get; set; }
        public required virtual ProductType ProductType { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
        public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
        public virtual ICollection<ProductBrand> ProductBrands { get; set; } = new List<ProductBrand>();

    }
}
