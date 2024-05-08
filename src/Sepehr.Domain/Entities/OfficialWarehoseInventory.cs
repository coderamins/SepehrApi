using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class OfficialWarehoseInventory : AuditableBaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public decimal Thickness { get; set; }
        public int WarehouseId { get; set; }
        public double ApproximateInventory { get; set; }
        public double FloorInventory { get; set; }
        public double MaxInventory { get; set; }
        public double MinInventory { get; set; }

        /// <summary>
        /// نقطه سفارش
        /// </summary>
        public double OrderPoint { get; set; }

        public required virtual Product ProductBrand { get; set; }
        public required virtual Warehouse Warehouse { get; set; }

    }
}
