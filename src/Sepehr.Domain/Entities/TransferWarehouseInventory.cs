using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class TransferWarehouseInventory:BaseEntity<int>
    {
        public int ProductBrantId { get; set; }
        public int OriginWarehouseId { get; set; }
        public int Amount { get; set; }

        public required virtual ProductBrand ProductBrand { get; set; }
    }
}
