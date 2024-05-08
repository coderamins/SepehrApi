using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class ProductInventoryViewModel
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseType { get; set; }
        public int ProductBrandId { get; set; }
        public decimal Thickness { get; set; }
        public decimal ApproximateInventory { get; set; }
        public double FloorInventory { get; set; }
        public double MaxInventory { get; set; }
        public double MinInventory { get; set; }

        public ProductBrandViewModel ProductBrand { get; set; }
        public ProductWarehouseViewModel ProductWarehouse { get; set; }
    }
}
