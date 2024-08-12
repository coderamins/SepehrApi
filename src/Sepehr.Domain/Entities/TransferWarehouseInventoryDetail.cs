using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class TransferWarehouseInventoryDetail:BaseEntity<int>
    {
        public int TransferWarehouseInventoryId { get; set; }
        public int ProductBrandId { get; set; }
        public decimal TransferAmount { get; set; }

        public virtual required ProductBrand ProductBrand { get; set; }
        public virtual required TransferWarehouseInventory TransferWarehouseInventory { get; set; }
    }
}
