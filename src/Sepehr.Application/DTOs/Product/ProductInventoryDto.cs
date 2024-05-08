using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Product
{
    public class ProductInventoryDto
    {
        public int WarehouseId { get; set; }
        public string WarehouseType { get; set; }
        public double Inventory { get; set; }
    }
}
