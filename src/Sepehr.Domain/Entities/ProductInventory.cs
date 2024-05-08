using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ProductInventory:AuditableBaseEntity<int>
    {
        public int ProductBrandId { get; set; }
        public int WarehouseId { get; set; }
        public decimal ApproximateInventory { get; set; }
        /// <summary>
        /// مووجودی در راه
        /// </summary>
        public decimal OnTransitInventory { get; set; }
        /// <summary>
        /// موجودی رزرو
        /// </summary>
        public decimal ReserveInventory { get; set; }
        /// <summary>
        /// موجودی خرید(مجازی)
        /// </summary>
        public decimal PurchaseInventory { get; set; }

        public double FloorInventory { get; set; }
        public double MaxInventory { get; set; }
        public double MinInventory { get; set; }
        public decimal Thickness { get; set; }
        /// <summary>
        /// نقطه سفارش
        /// </summary>
        public double OrderPoint { get; set; }

        public virtual ProductBrand ProductBrand { get; set; }
        public virtual Warehouse Warehouse { get; set; }

    }
}
